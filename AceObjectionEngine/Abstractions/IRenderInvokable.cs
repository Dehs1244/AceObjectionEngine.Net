using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Engine.Animator;
using AceObjectionEngine.Engine.Model;

namespace AceObjectionEngine.Abstractions
{
    /// <summary>
    /// Interface for implementing an object that will change the course of the render
    /// </summary>
    public interface IRenderInvokable
    {
        /// <summary>
        /// Called every time this object is animated
        /// </summary>
        /// <param name="sourceContext">Context of this object in rendering system</param>
        /// <param name="parallelObjects">Other objects that are animated in parallel</param>
        /// <returns>Information for the rendering system</returns>
        RenderActionConsequence OnRenderInvoke(AnimationRenderContext sourceContext, ICollection<IAnimationObject> parallelObjects);
    }
}
