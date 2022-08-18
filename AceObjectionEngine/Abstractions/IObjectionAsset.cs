using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Loader.Model;

namespace AceObjectionEngine.Abstractions
{
    /// <summary>
    /// Asset Loader Interface without typing
    /// </summary>
    public interface IObjectionAsset
    {
        /// <summary>
        /// Checking the loader container for its load
        /// </summary>
        bool Exists { get; }
        /// <summary>
        /// Loading all assets
        /// </summary>
        void DownloadAll();
    }

    /// <summary>
    /// Asset Loader Interface
    /// </summary>
    /// <typeparam name="T">The <see cref="IObjectionObject"/> that the loader will load</typeparam>
    public interface IObjectionAsset<T> : IObjectionAsset
        where T : IObjectionObject
    {
        /// <summary>
        /// All loaded assets
        /// </summary>
        IEnumerable<T> Loaded { get; }

        /// <summary>
        /// Loads a specific object using the Id
        /// </summary>
        /// <returns>Data of loaded <see cref="IObjectionObject"/></returns>
        LoaderData<T> Request();

        /// <summary>
        /// Loads a specific object directly using the Id
        /// </summary>
        /// <returns><see cref="IObjectionObject"/></returns>
        T Load();
        /// <summary>
        /// Loads all of objects
        /// </summary>
        /// <returns>Enumerable of loaded <see cref="IAnimationObject"/></returns>
        IEnumerable<T> LoadAll();
    }
}
