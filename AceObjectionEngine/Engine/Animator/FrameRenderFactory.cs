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
    public abstract class FrameRenderFactory : IFrameRenderer<Frame>
    {
        public IAnimator<Frame> Animator { get; }

        public int SpriteIteration { get; set; }
        public int AudioSourceIteration { get; set; }
        public TimeSpan TimeLineAnimationDuration { get; set; }
        public TimeSpan TimeLineRenderDuration { get; set; }

        public IAudioMixer AudioMixer { get; protected set; }

        public AnimatorRenderIterationsCollection<ISpriteSource> RenderedSprites { get; }

        public AnimatorRenderIterationsCollection<IAudioMixer> RenderedAudioMixers { get; }

        public FrameRenderFactory(ObjectionAnimator animator)
        {
            Animator = animator;
            RenderedAudioMixers = new AnimatorRenderIterationsCollection<IAudioMixer>();
            RenderedSprites = new AnimatorRenderIterationsCollection<ISpriteSource>();
        }

        public abstract Task<IAudioSource> RenderLayerAudioAsync(AnimationRenderContext audioSource);
        public abstract Task<IAudioSource> RenderAudioTickAsync(IAudioSource sprite, TimeSpan delay);
        public abstract Task RenderAllAsync();
        public abstract Task<ISpriteSource> RenderLayerSpriteAsync(ISpriteSource sprite);
        public abstract IAudioMixer CreateAudioMixer(TimeSpan timeLine);

        public virtual void RenderAll() => RenderAllAsync().GetAwaiter().GetResult();
        public virtual void RenderAudio(AnimationRenderContext[] layerAudioSources) => RenderAudioAsync(layerAudioSources).GetAwaiter().GetResult();
        public virtual void RenderSprite(AnimationRenderContext[] layerSprites) => RenderSpriteAsync(layerSprites).GetAwaiter().GetResult();

        public static IList<ISpriteSource> MergeAnimations(IList<ISpriteSource> firstFrames, IList<ISpriteSource> secondFrames, AnimationRenderContext context)
        {
            if (!firstFrames.Any()) return secondFrames;
            if (!secondFrames.Any()) return firstFrames;

            IList<ISpriteSource> renderedParallelAnimation = new List<ISpriteSource>();
            var firstFrameIteration = 0;
            var secondFrameIteration = 0;

            var processingCounter = context.Breaker == Attributes.AnimationStateBreaker.Parallel ? firstFrames.Count : secondFrames.Count;

            for (var i = 0; i < processingCounter; i++)
            {
                var passiveAnimationFrameError = context.Breaker == Attributes.AnimationStateBreaker.Parallel ?
                secondFrameIteration >= secondFrames.Count
                : firstFrameIteration >= firstFrames.Count;

                if (passiveAnimationFrameError)
                {
                    //if (context.Breaker == Attributes.AnimationStateBreaker.Origin) break;
                    switch (context.Breaker)
                    {
                        case Attributes.AnimationStateBreaker.Origin:
                            if (!context.RepeatOnBreak) secondFrames.Add((ISpriteSource)firstFrames.Last().Clone());
                            else firstFrameIteration = 0;
                            break;
                        case Attributes.AnimationStateBreaker.Parallel:
                            if (!context.RepeatOnBreak) secondFrames.Add((ISpriteSource)secondFrames.Last().Clone());
                            else secondFrameIteration = 0;
                            break;
                    }
                }
                var frame = (ISpriteSource)firstFrames[firstFrameIteration].Clone();

                renderedParallelAnimation.Add(frame.MergeSprite(secondFrames[secondFrameIteration]));

                secondFrameIteration++;
                firstFrameIteration++;
            }

            return renderedParallelAnimation;
        }

        public async Task RenderSpriteAsync(AnimationRenderContext[] layerSprites)
        {
            await Task.Run(async () =>
            {
                RenderedSprites.StartIteration(SpriteIteration);
                var behaviour = new ParallelRenderingBehaviour(this, layerSprites);

                ISpriteSource result = new Sprite(new System.Drawing.Bitmap(layerSprites.First().Sprite.Width, layerSprites.First().Sprite.Height));
                bool usedParallelRender = false;
                int toSkipRenders = 0;
                foreach(var layerSprite in layerSprites)
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

        public async Task RenderAudioAsync(AnimationRenderContext[] layerAudioSources)
        {
            await Task.Run(async () =>
            {
                for (var i = 0; i < layerAudioSources.Count(); i++)
                {
                    if (layerAudioSources[i].AudioTicks.Any())
                    {
                        for(var y = 0; y < layerAudioSources[i].AudioTicks.Count(); y++)
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
            RenderedAudioMixers.StartIteration(AudioSourceIteration);
            AudioMixer = CreateAudioMixer(timeLine);
            RenderedAudioMixers.Add(AudioMixer);
            RenderedAudioMixers.EndIteration();
            AudioSourceIteration++;
        }

        public void AddTime(TimeSpan time)
        {
            TimeLineAnimationDuration = TimeLineAnimationDuration.Add(time);
            TimeLineRenderDuration = TimeLineRenderDuration.Add(time);
        }

        public void Dispose()
        {
            foreach(var frame in RenderedSprites.MapAll())
            {
                frame.Dispose();
            }
        }
    }
}
