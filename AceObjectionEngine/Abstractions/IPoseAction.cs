using AceObjectionEngine.Engine.Animator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Engine.Model;

namespace AceObjectionEngine.Abstractions
{
    public interface IPoseAction
    {
        ISpriteSource State { get; set; }
        TimeSpan Delay { get; set; }
        TimeSpan Duration { get; }

        RenderActionConsequence Action(AnimationRenderContext sourceContext, ICollection<IAnimationObject> parallelObjects);
    }
}
