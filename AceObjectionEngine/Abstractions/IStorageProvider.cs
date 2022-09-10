using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Abstractions
{
    /// <summary>
    /// Storage Provider Implementation Interface
    /// Used for asset manager and presets
    /// </summary>
    public interface IStorageProvider
    {
        /// <summary>
        /// Create file
        /// </summary>
        /// <param name="path">Path to file</param>
        void CreatePoint(string path);
        /// <summary>
        /// Writing content in file
        /// </summary>
        /// <param name="point">Path to file</param>
        /// <param name="content">Content of file</param>
        void WriteContentInPoint(string point, string content);
        /// <summary>
        /// Check if file exists
        /// </summary>
        /// <param name="point">Path to file</param>
        /// <returns>True if file exists</returns>
        bool IsPointExists(string point);
        /// <summary>
        /// Reading content in file
        /// </summary>
        /// <param name="point">Path to file</param>
        /// <returns>Content of file</returns>
        string ReadPointContent(string point);
        /// <summary>
        /// Enumerabling all files in path
        /// </summary>
        /// <param name="route">Path</param>
        /// <returns>All points (files) in path</returns>
        IEnumerable<string> EnumerateAllPoints(string route);

        /// <summary>
        /// Creating directory
        /// </summary>
        /// <param name="route">Path to directory</param>
        void CreateRoute(string route);
        /// <summary>
        /// Removing directory
        /// </summary>
        /// <param name="route">Path to directory</param>
        void RemoveRoute(string route);
        /// <summary>
        /// Checking if directory exists
        /// </summary>
        /// <param name="route">Path to directory</param>
        /// <returns></returns>
        bool IsRouteExists(string route);
        /// <summary>
        /// Divides the path into subcategories
        /// </summary>
        /// <param name="route">Path</param>
        /// <returns>All subdirectories</returns>
        IEnumerable<string> SeparateRoute(string route);
        /// <summary>
        /// Enumerabling all directories in path
        /// </summary>
        /// <param name="inRoute">Path to directory</param>
        /// <returns>All directories in path</returns>
        IEnumerable<string> EnumerateAllRoutes(string inRoute);
    }
}
