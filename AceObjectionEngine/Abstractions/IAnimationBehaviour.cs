using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Abstractions
{
    /// <summary>
    /// Implements methods for animating objects on a frame
    /// </summary>
    public interface IAnimationBehaviour
    {
        /// <summary>
        /// Called every time, when behaviour is using
        /// </summary>
        /// <param name="index">Render indexer</param>
        /// <returns></returns>
        ISpriteSource[] Use(int index);
        /// <summary>
        /// Asynchronously called every time, when behaviour is using
        /// </summary>
        /// <param name="index">Render indexer</param>
        /// <returns></returns>
        Task<ISpriteSource[]> UseAsync(int index);
    }
}
