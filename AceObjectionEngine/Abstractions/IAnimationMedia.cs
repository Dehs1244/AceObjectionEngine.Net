using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Abstractions
{
    /// <summary>
    /// Interface for implementation media objects
    /// </summary>
    public interface IAnimationMedia
    {
        /// <summary>
        /// Media Format
        /// </summary>
        string Format { get; }
        /// <summary>
        /// Duration Of Media
        /// </summary>
        TimeSpan Duration { get; }
    }
}
