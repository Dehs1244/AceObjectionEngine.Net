using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Abstractions;
using AceObjectionEngine.Engine.Animator;

namespace AceObjectionEngine.Examples.ImplementationExamples
{
    public class FrameRenderImplementationExample : FrameRenderFactory
    {
        public FrameRenderImplementationExample(ObjectionAnimator animator) : base(animator)
        {
        }

        public override IAudioMixer CreateAudioMixer(TimeSpan timeLine)
        {
            //Creating Audio Mixer for Rendering
            throw new NotImplementedException();
        }

        public override Task RenderAllAsync()
        {
            //The end point of the render, collects all the frames into a single video
            throw new NotImplementedException();
        }

        public override Task<IAudioSource> RenderAudioTickAsync(IAudioSource sprite, TimeSpan delay)
        {
            //Processing of single audio elements
            throw new NotImplementedException();
        }

        public override Task<IAudioSource> RenderLayerAudioAsync(AnimationRenderContext audioSource)
        {
            //Processing of elements with an audio component
            throw new NotImplementedException();
        }

        public override Task<ISpriteSource> RenderLayerSpriteAsync(ISpriteSource sprite)
        {
            //Some actions with sprite
            throw new NotImplementedException();
        }
    }
}
