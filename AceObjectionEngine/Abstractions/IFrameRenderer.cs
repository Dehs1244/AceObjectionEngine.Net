using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Abstractions.Async;
using AceObjectionEngine.Engine.Animator;
using AceObjectionEngine.Engine.Model;

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
        /// Method of animating objects on the frame
        /// Recreated with each layer rendering
        /// </summary>
        //IAnimationBehaviour Behaviour { get; set; }

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
        /// Rendering single audio on audio mixer
        /// </summary>
        /// <param name="audio">Audio to render</param>
        /// <param name="timeLine">Time where start playing audio</param>
        void RenderSingleAudio(IAudioSource audio, TimeSpan timeLine);
        /// <summary>
        /// Reset Audio Mixer of frame render
        /// </summary>
        void ResetAudioMixer(TimeSpan timeLine);

        /// <summary>
        /// Rendering the current fragment and add it to fragments pool
        /// </summary>
        void RenderFragment();

        /// <summary>
        /// Render of all rendered objects into a stream
        /// </summary>
        /// <param name="context">Information for the final rendering of all fragments of animator</param>
        void RenderAll(RenderingEndPointContext context);
    }
}
