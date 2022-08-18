using AceObjectionEngine.Abstractions;
using AceObjectionEngine.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Engine.Attributes
{
    /// <summary>
    /// Breakers of animation
    /// </summary>
    /// 
    public enum AnimationStateBreaker
    {
        /// <summary>
        /// The one that started the parallel animation
        /// </summary>
        Origin,
        /// <summary>
        /// An object that animates with another object
        /// </summary>
        Parallel
    }

    /// <summary>
    /// Settings for parallel animation
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface)]
    public class ParallelAnimationOptionsAttribute : Attribute
    {
        /// <summary>
        /// The culprit of the end of the animation
        /// </summary>
        public AnimationStateBreaker SourceBreaker;
        /// <summary>
        /// Allows the culprit to re-play the animation
        /// </summary>
        public bool RepeatOnBreak;
        /// <summary>
        /// Is skip next render
        /// </summary>
        public bool IsSkip;
        /// <summary>
        /// Sets the value of the breaker only for the specified types, otherwise inverts the value
        /// </summary>
        public Type[] BreakOnlyFor;

        public static ParallelAnimationOptionsAttribute Default => new ParallelAnimationOptionsAttribute()
        {
            SourceBreaker = AnimationStateBreaker.Origin,
            RepeatOnBreak = true,
            BreakOnlyFor = Array.Empty<Type>(),
            IsSkip = true
        };
        
        public ParallelAnimationOptionsAttribute(AnimationStateBreaker breaker = AnimationStateBreaker.Origin, bool repeatOnBreak = true, bool isSkipRender = true, params Type[] breakOnlyFor)
        {
            SourceBreaker = breaker;
            RepeatOnBreak = repeatOnBreak;
            IsSkip = isSkipRender;
            BreakOnlyFor = breakOnlyFor;
        }

        public AnimationStateBreaker InvertBreaker() => SourceBreaker == AnimationStateBreaker.Origin ? AnimationStateBreaker.Parallel : AnimationStateBreaker.Origin;

        public bool CheckForBreakOnly(IAnimationObject mergingObject) => BreakOnlyFor.Any() ?
            BreakOnlyFor.Any(x => TypeHelper.AreSame(x, mergingObject.GetType())) : true;
    }
}
