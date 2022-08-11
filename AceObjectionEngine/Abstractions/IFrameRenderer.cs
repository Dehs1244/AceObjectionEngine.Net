using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Abstractions.Async;
using AceObjectionEngine.Engine.Animator;

namespace AceObjectionEngine.Abstractions
{
    public interface IFrameRenderer<T> : IFrameRendererAsync<T>, IDisposable
        where T : class, IAnimatorHierarchy<T>
    {
        /// <summary>
        /// Helps to render audio (Mix, merge and other)
        /// </summary>
        IAudioMixer AudioMixer { get; }
        /// <summary>
        /// Render All Animation Objects from a Layer to single frame
        /// </summary>
        /// <param name="layerSprites">Animation Objects from processed layer</param>
        void RenderSprite(AnimationRenderContext[] layerSprites);
        /// <summary>
        /// Render All Audio Sources from a Layer to single audio
        /// </summary>
        /// <param name="layerAudioSources">Audio Sources from processed layer</param>
        void RenderAudio(AnimationRenderContext[] layerAudioSources);
        /// <summary>
        /// Reset Audio Mixer of frame render
        /// </summary>
        void ResetAudioMixer(TimeSpan timeLine);

        /// <summary>
        /// Render of all rendered objects into a stream
        /// </summary>
        void RenderAll();
    }
}
