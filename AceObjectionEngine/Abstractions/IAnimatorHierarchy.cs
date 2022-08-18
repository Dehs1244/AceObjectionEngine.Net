using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Engine.Collections;

namespace AceObjectionEngine.Abstractions
{
    /// <summary>
    /// Interface hierarchy of storage of animated objects
    /// </summary>
    /// <typeparam name="T">The hierarchy itself</typeparam>
    public interface IAnimatorHierarchy<T> : IEnumerable<IAnimationObject>
        where T : class, IAnimatorHierarchy<T>
    {
        /// <summary>
        /// Animation Updater
        /// </summary>
        int Updater { get; }

        /// <summary>
        /// Duration of the animation hierarchy
        /// </summary>
        TimeSpan Duration { get; }
        /// <summary>
        /// All animation objects in hierarchy
        /// </summary>
        AnimatorLayersCollection<T> Objects { get; }
        /// <summary>
        /// Updates the hierarchy with each successful rendering
        /// </summary>
        /// <returns>Need for further rendering of the hierarchy</returns>
        bool UpdateHierarchy();
        /// <summary>
        /// Able to read parallel objects
        /// </summary>
        /// <returns></returns>
        bool IsCanReadParallelObjects();
    }
}
