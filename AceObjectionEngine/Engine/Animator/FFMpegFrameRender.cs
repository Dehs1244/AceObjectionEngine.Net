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

namespace AceObjectionEngine.Engine.Animator
{
    public class FFMpegFrameRender : FrameRenderFactory
    {
        private StreamPipeSink _pipeStream { get; }

        public FFMpegFrameRender(ObjectionAnimator animator, Stream outputStream) : base(animator)
        {
            _pipeStream = new StreamPipeSink(outputStream);
        }

        public override async Task RenderAllAsync()
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

            foreach(var audioMixer in RenderedAudioMixers.MapAll())
            {
                if(audio == null)
                {
                    audio = audioMixer.Create();
                }
                else
                {
                    audio = audio.Merge(audioMixer.Create());
                }
            }
            audio = audio.SetDuration(TimeLineAnimationDuration);

            if (audio != null) factory.AddFileInput(audio.FilePath);

            await factory?.OutputToPipe(_pipeStream, options =>
            {
                options.WithVideoCodec("vp9")
                .ForceFormat("webm");
            }).ProcessAsynchronously();

            audio.Dispose();
        }

        public override Task<ISpriteSource> RenderLayerSpriteAsync(ISpriteSource sprite)
        {
            return Task.FromResult(sprite);
        }

        public override async Task<IAudioSource> RenderAudioTickAsync(IAudioSource audio, TimeSpan delay)
        {
            var tempFile = new TempFileStream("OBJECTION-AUDIO-RENDER.mp3");
            tempFile.Close();

            await FFMpegArguments
            .FromFileInput(audio.FilePath)
            .OutputToFile(tempFile.FilePath, true, options =>
            {
                if (audio.Offset.Ticks > 0)
                    options.WithCustomArgument($"-af areverse,apad=pad_dur={(int)audio.Offset.TotalMilliseconds}ms,areverse");

                options
                .WithAudioBitrate((int)audio.BitRate);
            })
            .ProcessAsynchronously();

            var tempFileDelay = new TempFileStream("OBJECTION-AUDIO-RENDER.mp3");
            tempFileDelay.Close();

            await FFMpegArguments
            .FromFileInput(tempFile.FilePath)
            .OutputToFile(tempFileDelay.FilePath, true, options =>
            {
                if (delay.Ticks > 0)
                    options.WithCustomArgument($"-af areverse,apad=pad_dur={(int)delay.TotalMilliseconds}ms,areverse");

                options
                .WithAudioBitrate((int)audio.BitRate);
            })
            .ProcessAsynchronously();

            tempFile.Dispose();
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
            if(audioSource.EndAnimationDuration.Ticks > 0)
                animationAudioSource = animationAudioSource.SetDuration(audioSource.EndAnimationDuration);

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

        public override IAudioMixer CreateAudioMixer(TimeSpan timeLine) => new FFMpegAudioMixer(timeLine);
    }
}
