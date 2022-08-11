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
        public AnimationStateBreaker Breaker;
        /// <summary>
        /// Allows the culprit to re-play the animation
        /// </summary>
        public bool RepeatOnBreak;
        /// <summary>
        /// Is skip next render
        /// </summary>
        public bool IsSkip;

        public static ParallelAnimationOptionsAttribute Default => new ParallelAnimationOptionsAttribute()
        {
            Breaker = AnimationStateBreaker.Origin,
            RepeatOnBreak = true,
            IsSkip = true
        };
        
        public ParallelAnimationOptionsAttribute(AnimationStateBreaker breaker = AnimationStateBreaker.Origin, bool repeatOnBreak = true, bool isSkipRender = true)
        {
            Breaker = breaker;
            RepeatOnBreak = repeatOnBreak;
            IsSkip = isSkipRender;
        }
    }
}
