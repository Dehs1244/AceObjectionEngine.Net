using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Engine.Animator;
using AceObjectionEngine.Engine.Collections;

namespace AceObjectionEngine.Abstractions.Async
{
    public interface IFrameRendererAsync<T>
        where T : class, IAnimatorHierarchy<T>
    {
        /// <summary>
        /// Animator that processes frames
        /// </summary>
        IAnimator<T> Animator { get; }
        /// <summary>
        /// Iteration on every render sprite 
        /// </summary>
        int SpriteIteration { get; set; }
        /// <summary>
        /// Iteration on every render audio
        /// </summary>
        int AudioSourceIteration { get; set; }
        /// <summary>
        /// All Rendered Sprites, which were obtained using the method "RenderSprite"
        /// </summary>
        AnimatorRenderIterationsCollection<ISpriteSource> RenderedSprites { get; }
        /// <summary>
        /// All Rendered Audio, which were obtained using the method "RenderSprite"
        /// </summary>
        AnimatorRenderIterationsCollection<IAudioMixer> RenderedAudioMixers { get; }

        /// <summary>
        /// Asynchronously Render All Animation Objects from a Layer to single frame
        /// </summary>
        /// <param name="layerSprites">Animation Objects from processed layer</param>
        Task RenderSpriteAsync(AnimationRenderContext[] layerSprites);
        /// <summary>
        /// Asynchronously Render All Audio Sources from a Layer to single audio
        /// </summary>
        /// <param name="layerAudioSources">Audio Sources from processed layer</param>
        Task RenderAudioAsync(AnimationRenderContext[] layerAudioSources);

        /// <summary>
        /// Asynchronously Render of all rendered objects into a stream
        /// </summary>
        Task RenderAllAsync();
    }
}
