using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Abstractions;
using AceObjectionEngine.Engine.Animator;
using AceObjectionEngine.Engine.Model;

namespace AceObjectionEngine.Engine.PoseActions
{
    public class SpeakingPoseAction : IPoseAction
    {
        public ISpriteSource State { get; set; }
        public TimeSpan Delay { get; set; }
        public TimeSpan Duration => TimeSpan.Zero;

        public RenderActionConsequence Action(AnimationRenderContext sourceContext, ICollection<IAnimationObject> parallelObjects)
            => new RenderActionConsequence(State.AnimateFrames());
    }
}
