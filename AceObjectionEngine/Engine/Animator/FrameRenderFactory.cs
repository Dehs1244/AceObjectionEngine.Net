using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Abstractions;
using AceObjectionEngine.Engine.Collections;
using AceObjectionEngine.Helpers;
using AceObjectionEngine.Engine.AudioMixers;
using AceObjectionEngine.Engine.Model;
using AceObjectionEngine.Engine.Animator.Behaviours;

namespace AceObjectionEngine.Engine.Animator
{
    /// <summary>
    /// Abstract class with support for rendering layers and using parallel rendering
    /// </summary>
    /// <typeparam name="TFrameFragment">Frame rendering fragment</typeparam>
    public abstract class FrameRenderFactory<TFrameFragment> : IFrameRenderer<Frame>, IAudioMixerCreator
        where TFrameFragment : IFrameFragment
    {
        public IAnimator<Frame> Animator { get; }

        public int SpriteIteration { get; set; }
        public int AudioSourceIteration { get; set; }
        public TimeSpan TimeLineAnimationDuration { get; set; }
        public TimeSpan TimeLineRenderDuration { get; set; }

        public IAudioMixer AudioMixer => RenderedAudioMixers.Last();

        public AnimatorRenderIterationsCollection<ISpriteSource> RenderedSprites { get; }

        public AnimatorRenderIterationsCollection<IAudioMixer> RenderedAudioMixers { get; }

        protected TFrameFragment BakedRenderFragment { get; private set; }

        public FrameRenderFactory(ObjectionAnimator animator)
        {
            Animator = animator;
            RenderedAudioMixers = new AnimatorRenderIterationsCollection<IAudioMixer>();
            RenderedSprites = new AnimatorRenderIterationsCollection<ISpriteSource>();
        }

        public abstract Task<IAudioSource> RenderLayerAudioAsync(AnimationRenderContext audioSource);
        public abstract Task<IAudioSource> RenderAudioTickAsync(IAudioSource sprite, TimeSpan delay);
        public abstract Task RenderAllAsync(RenderingEndPointContext context);
        public abstract Task<ISpriteSource> RenderLayerSpriteAsync(ISpriteSource sprite);
        public abstract IAudioMixer CreateAudioMixer(TimeSpan timeLine);
        public abstract Task<TFrameFragment> RenderFragmentInnerAsync();

        protected abstract Task<TFrameFragment> BakeFrameFragmentAsync(TFrameFragment processedFragment);

        public virtual void RenderAll(RenderingEndPointContext context) => RenderAllAsync(context).GetAwaiter().GetResult();
        public virtual void RenderAudio(AnimationRenderContext[] layerAudioSources) => RenderAudioAsync(layerAudioSources).GetAwaiter().GetResult();
        public virtual void RenderFragment() => RenderFragmentAsync().GetAwaiter().GetResult();
        public void RenderSingleAudio(IAudioSource audio, TimeSpan timeLine) => RenderSingleAudioAsync(audio, timeLine).GetAwaiter().GetResult();
        public virtual void RenderSprite(AnimationRenderContext[] layerSprites) => RenderSpriteAsync(layerSprites).GetAwaiter().GetResult();

        public async Task RenderSpriteAsync(AnimationRenderContext[] layerSprites)
        {
            await Task.Run(async () =>
            {
                RenderedSprites.StartIteration(SpriteIteration);
                var behaviour = new ParallelRenderingBehaviour<TFrameFragment>(this, layerSprites);

                ISpriteSource result = new Sprite(new System.Drawing.Bitmap(layerSprites.First().Sprite.Width, layerSprites.First().Sprite.Height));
                bool usedParallelRender = false;
                int toSkipRenders = 0;
                foreach (var layerSprite in layerSprites)
                {
                    await layerSprite.AnimationObject.StartAnimationAsync();
                }

                for (var i = 0; i < layerSprites.Count(); i++)
                {
                    usedParallelRender = false;

                    var animated = behaviour.Use(i);
                    toSkipRenders = behaviour.RendersToSkip;
                    //if (layerSprites[i].CanParallelRender()) nextRenderSkip = true;
                    usedParallelRender = true;
                    ISpriteSource coreAnimationFrame = (ISpriteSource)result.Clone();

                    foreach (var animatedFrame in animated)
                    {
                        var renderingFrame = (ISpriteSource)coreAnimationFrame.Clone();
                        renderingFrame.MergeSprite(animatedFrame);
                        await RenderLayerSpriteAsync(renderingFrame);

                        RenderedSprites.Add((ISpriteSource)renderingFrame.Clone());
                        renderingFrame.Dispose();
                    }
                    //AddTime(Frame.CalculateDuration(animated.Count()));
                    coreAnimationFrame.Dispose();

                    if (usedParallelRender) i = toSkipRenders + 1;
                }
                RenderedSprites.EndIteration();
                SpriteIteration++;
            });
        }

        public async Task RenderSingleAudioAsync(IAudioSource audio, TimeSpan timeLine)
        {
            IAudioSource audioRendered = await RenderAudioTickAsync(audio, timeLine);
            AudioMixer.Mix(ref audioRendered);
            audioRendered.Dispose();
        }

        public async Task RenderFragmentAsync()
        {
            var rendered = await RenderFragmentInnerAsync();
            BakedRenderFragment = await BakeFrameFragmentAsync(rendered);
            RenderedAudioMixers.Clear();
            RenderedSprites.Clear();
        }

        public async Task RenderAudioAsync(AnimationRenderContext[] layerAudioSources)
        {
            await Task.Run(async () =>
            {
                for (var i = 0; i < layerAudioSources.Count(); i++)
                {
                    if (layerAudioSources[i].AudioTicks.Any())
                    {
                        for (var y = 0; y < layerAudioSources[i].AudioTicks.Count(); y++)
                        {
                            IAudioSource audioTickRender = await RenderAudioTickAsync(layerAudioSources[i].AudioTicks[y], TimeLineRenderDuration);
                            AudioMixer.Mix(ref audioTickRender);
                            audioTickRender.Dispose();
                        }
                    }

                    if (layerAudioSources[i].AudioSource != null)
                    {
                        if (layerAudioSources[i].EndAnimationDuration.Ticks < 0)
                            layerAudioSources[i].EndAnimationDuration = layerAudioSources[i].EndRenderFrameDuration;

                        IAudioSource audioSource = await RenderLayerAudioAsync(layerAudioSources[i]);
                        AudioMixer.Mix(ref audioSource);
                        audioSource.Dispose();
                    }
                }
                AudioSourceIteration++;
            });
        }

        public void ResetAudioMixer(TimeSpan timeLine)
        {
            TimeLineRenderDuration = TimeSpan.Zero;
            if (!RenderedAudioMixers.IsReadOnly) RenderedAudioMixers.EndIteration();
            RenderedAudioMixers.StartIteration(AudioSourceIteration);
            RenderedAudioMixers.Add(CreateAudioMixer(timeLine));
            AudioSourceIteration++;
        }

        public void AddTime(TimeSpan time)
        {
            TimeLineAnimationDuration = TimeLineAnimationDuration.Add(time);
            TimeLineRenderDuration = TimeLineRenderDuration.Add(time);
        }

        public void Dispose()
        {
            foreach (var frame in RenderedSprites.MapAll())
            {
                frame.Dispose();
            }
            BakedRenderFragment?.Dispose();
        }
    }
}
