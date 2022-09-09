using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Abstractions;
using FFMpegCore.Pipes;
using FFMpegCore;
using AceObjectionEngine.Engine.Model;
using AceObjectionEngine.Helpers;
using AceObjectionEngine.Engine.Collections;
using AceObjectionEngine.Engine.AudioMixers;
using FFMpegCore.Enums;

namespace AceObjectionEngine.Engine.Animator
{
    public class FFMpegFrameRender : FrameRenderFactory<FrameFragment>
    {
        private Stream _sourceStream { get; }

        public FFMpegFrameRender(ObjectionAnimator animator, Stream outputStream) : base(animator)
        {
            _sourceStream = outputStream;
        }

        public override async Task RenderAllAsync(RenderingEndPointContext context)
        {
            FFMpegArguments factory = null;
            IAudioSource audio = context.MainMixer.Create();
            var bakedRenderFragmentMp4 = new TempFileStream("baked-frag.mp4");
            bakedRenderFragmentMp4.Close();

            if (BakedRenderFragment != null)
            {
                await FFMpegArguments.FromFileInput(BakedRenderFragment.FilePath)
                .OutputToFile(bakedRenderFragmentMp4.FilePath, true, options =>
                {
                    options.CopyChannel();
                })
                .ProcessAsynchronously();

                factory = FFMpegArguments.FromFileInput(bakedRenderFragmentMp4.FilePath);
            }
            audio = audio.SetDuration(TimeLineAnimationDuration);

            if (audio != null) factory.AddFileInput(audio.FilePath);

            FileStream tempCompletedResult = new TempFileStream("OBJECTION-COMPLETED.mp4");
            tempCompletedResult.Close();

            await factory?.OutputToFile(tempCompletedResult.Name, true, options =>
            {
                options
                .WithFramerate((int)ObjectionAnimator.FrameRate)
                .WithDuration(TimeLineAnimationDuration)
                .WithCustomArgument("-filter_complex \"[0:a][1:a]amerge,pan=stereo|c0<c0+c2|c1<c1+c3[a]\" -map 0:v -map \"[a]\" -c:v copy -c:a aac -shortest");
            }).ProcessAsynchronously();

            tempCompletedResult = File.OpenRead(tempCompletedResult.Name);

            tempCompletedResult.Seek(0, SeekOrigin.Begin);
            tempCompletedResult.CopyTo(_sourceStream);

            tempCompletedResult.Dispose();
            bakedRenderFragmentMp4.Dispose();

            audio.Dispose();
        }

        public override async Task<FrameFragment> RenderFragmentInnerAsync()
        {
            var renderedVideoFrames = RenderedSprites.MapAll().OfType<IVideoFrame>();
            FFMpegArguments factory = null;

            if (renderedVideoFrames.Any())
            {
                RawVideoPipeSource videoFrames = new RawVideoPipeSource(renderedVideoFrames);
                videoFrames.FrameRate = ObjectionAnimator.FrameRate;

                factory = FFMpegArguments
                    .FromPipeInput(videoFrames, (options) =>
                    {
                        options.Resize(Animator.Width, Animator.Height);
                    });
            }
            IAudioSource audio = null;

            foreach (var audioMixer in RenderedAudioMixers.MapAll())
            {
                if (audio == null)
                {
                    audio = audioMixer.Create();
                }
                else
                {
                    audio = audio.Merge(audioMixer.Create());
                }
            }

            if (audio != null)
            {
                audio = audio.SetDuration(TimeLineRenderDuration);
                factory.AddFileInput(audio.FilePath);
            }

            var tempFragmentWebm = new TempFileStream("frag.webm");
            Stream spanStream = new MemoryStream();

            await factory?.OutputToPipe(new StreamPipeSink(spanStream), options =>
            {
                options
                .WithVideoCodec("vp9")
                .ForceFormat("webm");
            }).ProcessAsynchronously();
            audio.Dispose();

            spanStream.Seek(0, SeekOrigin.Begin);
            spanStream.CopyTo(tempFragmentWebm);
            spanStream.Dispose();

            tempFragmentWebm.Close();

            return new FrameFragment(tempFragmentWebm.FilePath);
        }

        public override Task<ISpriteSource> RenderLayerSpriteAsync(ISpriteSource sprite)
        {
            return Task.FromResult(sprite);
        }

        public override async Task<IAudioSource> RenderAudioTickAsync(IAudioSource audio, TimeSpan delay)
        {
            audio = audio.AddOffset(audio.Offset);

            var tempFileDelay = new TempFileStream("OBJECTION-AUDIO-RENDER.mp3");
            tempFileDelay.Close();

            await FFMpegArguments
            .FromFileInput(audio.FilePath)
            .OutputToFile(tempFileDelay.FilePath, true, options =>
            {
                if (delay.Ticks > 0)
                    options.WithCustomArgument($"-af areverse,apad=pad_dur={(int)delay.TotalMilliseconds}ms,areverse");

                options
                .WithAudioBitrate((int)audio.BitRate);
            })
            .ProcessAsynchronously();

            audio.Dispose();
            IAudioSource renderedAudio = new AudioSource(tempFileDelay);

            return renderedAudio;
        }

        public override async Task<IAudioSource> RenderLayerAudioAsync(AnimationRenderContext audioSource)
        {
            var tempFile = new TempFileStream("OBJECTION-AUDIO-RENDER.mp3");
            tempFile.Close();
            //Issue: Sample reading error and noise appearance
            //Solution: Direct file reading
            IAudioSource animationAudioSource = audioSource.AudioSource;
            if (audioSource.AudioSource.IsFixate && audioSource.EndRenderFrameDuration.Ticks > 0)
            {
                if(audioSource.EndRenderFrameDuration > animationAudioSource.Duration)
                {
                    var series = 1;
                    while(TimeSpan.FromTicks(animationAudioSource.Duration.Ticks * series) < audioSource.EndRenderFrameDuration)
                    {
                        series++;
                    }

                    animationAudioSource = animationAudioSource.Series(series);
                }
                animationAudioSource = animationAudioSource.SetDuration(audioSource.EndRenderFrameDuration);
            }

            await FFMpegArguments
            .FromFileInput(animationAudioSource.FilePath)
            .OutputToFile(tempFile.FilePath, true, options =>
            {
                if (audioSource.CurrentRenderFrameDuration.TotalMilliseconds > 0)
                    options.WithCustomArgument($"-af areverse,apad=pad_dur={(int)audioSource.CurrentRenderFrameDuration.TotalMilliseconds}ms,areverse");
                options
                .WithAudioBitrate((int)audioSource.AudioSource.BitRate);
            })
            .ProcessAsynchronously();

            animationAudioSource.Dispose();
            IAudioSource renderedAudio = new AudioSource(tempFile);

            //Pipe is shit, better using temp files
            return renderedAudio;
        }

        protected override async Task<FrameFragment> BakeFrameFragmentAsync(FrameFragment processedFragment)
        {
            if (BakedRenderFragment == null) return processedFragment;

            var bakedTempFile = new TempFileStream("baked-frag.webm");
            bakedTempFile.Close();

            await FFMpegArguments.FromFileInput(BakedRenderFragment.FilePath)
                .AddFileInput(processedFragment.FilePath)
                .OutputToFile(bakedTempFile.FilePath, true, options =>
                {
                    options.WithCustomArgument("-filter_complex \"[0:v][0:a][1:v][1:a] concat=n=2:v=1:a=1[v][a]\" -map \"[v]\" -map \"[a]\"");
                }).ProcessAsynchronously();

            BakedRenderFragment?.Dispose();
            processedFragment?.Dispose();
            return new FrameFragment(bakedTempFile.FilePath);
        }

        public override IAudioMixer CreateAudioMixer(TimeSpan timeLine) => new FFMpegAudioMixer(timeLine);
    }
}
