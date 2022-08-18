using AceObjectionEngine.Engine.Animator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Engine.Model;

namespace AceObjectionEngine.Abstractions
{
    /// <summary>
    /// Interface for implementing branches from the main render and processing them in another branch
    /// </summary>
    public interface IRenderBranch
    {
        /// <summary>
        /// Sprite of current action
        /// </summary>
        ISpriteSource State { get; set; }
        /// <summary>
        /// Delay of <see cref="State"/>
        /// </summary>
        TimeSpan Delay { get; set; }
        /// <summary>
        /// Duration of action
        /// </summary>
        TimeSpan Duration { get; }

        /// <summary>
        /// Called every time the animation of this branch starts
        /// </summary>
        /// <param name="sourceContext">The source of this pose action</param>
        /// <param name="parallelObjects">Other objects that are animated in parallel</param>
        /// <returns>Information for the rendering system</returns>
        RenderActionConsequence Action(AnimationRenderContext sourceContext, ICollection<IAnimationObject> parallelObjects);
    }
}
