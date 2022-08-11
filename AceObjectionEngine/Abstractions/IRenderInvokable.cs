using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Engine.Animator;
using AceObjectionEngine.Engine.Model;

namespace AceObjectionEngine.Abstractions
{
    public interface IRenderInvokable
    {
        RenderActionConsequence OnRenderInvoke(AnimationRenderContext sourceContext, ICollection<IAnimationObject> parallelObjects);
    }
}
