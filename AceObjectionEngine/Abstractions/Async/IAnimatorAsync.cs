using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Abstractions.Async
{
    /// <summary>
    /// Interface for implementation animator to animate <see cref="IAnimationObject"/>
    /// </summary>
    /// <typeparam name="T">Hierarchy of frames</typeparam>
    public interface IAnimatorAsync<T>
         where T : class, IAnimatorHierarchy<T>
    {
        /// <summary>
        /// Hierarchy of animation objects
        /// </summary>
        ICollection<T> Hierarchy { get; }
        Stream OutputStream { get; }
        /// <summary>
        /// Animation asynchronously start method frame hierarchy
        /// </summary>
        Task AnimateAsync();
    }
}
