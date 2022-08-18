using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Abstractions;
using AceObjectionEngine.Engine.Animator;
using AceObjectionEngine.Engine.Model;

namespace AceObjectionEngine.Examples.Components
{
    public class RenderBranchExample : IRenderBranch
    {
        public ISpriteSource State { get; set; }
        public TimeSpan Delay { get; set; }

        public TimeSpan Duration => throw new NotImplementedException();

        public RenderActionConsequence Action(AnimationRenderContext sourceContext, ICollection<IAnimationObject> parallelObjects)
        {
            throw new NotImplementedException();
        }
    }
}
