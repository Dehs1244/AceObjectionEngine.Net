using AceObjectionEngine.Abstractions;
using AceObjectionEngine.Engine.Animator;
using AceObjectionEngine.Engine.Model;
using AceObjectionEngine.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Engine.PoseActions
{
    /// <summary>
    /// The action pose, but without counts
    /// </summary>
    public class SinglePoseAction : IRenderBranch
    {
        public ISpriteSource State { get; set; }
        public TimeSpan Delay { get; set; }

        private TimeSpan _singleDuration;
        private bool _isAlreadyCountingDuration;
        public TimeSpan Duration { get
            {
                if (_isAlreadyCountingDuration) return TimeSpan.Zero;
                _isAlreadyCountingDuration = true;
                return _singleDuration;
            } }

        public RenderActionConsequence Action(AnimationRenderContext sourceContext, ICollection<IAnimationObject> parallelObjects)
            => new RenderActionConsequence(State.AnimateFrames().WithDelay(Delay, State.DelayMode));
    }
}
