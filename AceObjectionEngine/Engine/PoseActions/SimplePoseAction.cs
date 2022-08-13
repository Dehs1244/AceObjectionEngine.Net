using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Abstractions;
using AceObjectionEngine.Engine.Animator;
using AceObjectionEngine.Engine.Model;
using AceObjectionEngine.Extensions;

namespace AceObjectionEngine.Engine.PoseActions
{
    public class SimplePoseAction : IPoseAction
    {
        public ISpriteSource State { get; set; }
        public TimeSpan Delay { get; set; }
        public TimeSpan Duration => State.Duration;

        RenderActionConsequence IPoseAction.Action(AnimationRenderContext sourceContext, ICollection<IAnimationObject> parallelObjects)
            => new RenderActionConsequence(State.AnimateFrames().WithDelay(Delay, State.DelayMode));
    }
}
