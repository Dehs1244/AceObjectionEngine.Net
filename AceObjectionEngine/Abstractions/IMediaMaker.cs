using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Abstractions
{
    /// <summary>
    /// Interface for implementing creation animation media
    /// </summary>
    /// <typeparam name="T">Animation media being created</typeparam>
    public interface IMediaMaker<T> where T : IAnimationMedia
    {
        /// <summary>
        /// Create media from file
        /// </summary>
        /// <param name="filePath">Path to media file</param>
        /// <returns><see cref="IAnimationMedia"/></returns>
        T Make(string filePath);
        /// <summary>
        /// Create media from file with arguments
        /// </summary>
        /// <param name="filePath">Path to media file</param>
        /// <param name="args">Arguments to maker</param>
        /// <returns><see cref="IAnimationMedia"/></returns>
        T Make(string filePath, params object[] args);
    }
}
