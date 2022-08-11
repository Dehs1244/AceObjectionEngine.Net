using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Abstractions.Async
{
    /// <summary>
    /// Interface for implementing an animated object
    /// </summary>
    public interface IAnimationObjectAsync
    {
        /// <summary>
        /// An animation object display sprite
        /// </summary>
        ISpriteSource Sprite { get; }
        /// <summary>
        /// The sound source of the animated object
        /// </summary>
        IAudioSource AudioSource { get; }
        /// <summary>
        /// Duration for animation object duration reader
        /// </summary>
        TimeSpan DurationCounter { get; }

        /// <summary>
        /// Asynchronous called at each end of the animation
        /// </summary>
        /// <returns></returns>
        Task EndAnimationAsync();
    }
}
