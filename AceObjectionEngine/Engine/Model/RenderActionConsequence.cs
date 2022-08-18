using AceObjectionEngine.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Engine.Model
{
    /// <summary>
    /// Description inforamtion of the branch for the render
    /// </summary>
    public struct RenderActionConsequence
    {
        /// <summary>
        /// Denotes an action in a branch
        /// </summary>
        public bool IsAction { get; }
        /// <summary>
        /// New rendered animation in a branch
        /// </summary>
        public ISpriteSource[] Animation { get; }
        /// <summary>
        /// Closed objects for the current rendering
        /// </summary>
        public Type[] ClosedForRender { get; }

        public RenderActionConsequence(ISpriteSource[] animation)
        {
            IsAction = animation.Count() > 0;
            Animation = animation;
            ClosedForRender = Array.Empty<Type>();
        }

        public RenderActionConsequence(IEnumerable<ISpriteSource> animation) : this(animation.ToArray())
        {

        }

        public RenderActionConsequence(ISpriteSource[] animation, params IAnimationObject[] closedForRender)
        {
            IsAction = animation.Count() > 0;
            Animation = animation;
            ClosedForRender = closedForRender.Select(x=> x.GetType()).ToArray();
        }

        public RenderActionConsequence(IEnumerable<ISpriteSource> animation, params IAnimationObject[] closedForRender) : this(animation.ToArray(), closedForRender)
        {

        }

        public static RenderActionConsequence FromEmpty() => new RenderActionConsequence(Array.Empty<ISpriteSource>());
    }
}
