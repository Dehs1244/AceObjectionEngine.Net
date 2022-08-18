using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Abstractions.Async
{
    /// <summary>
    /// Interface for creating a preset loader
    /// </summary>
    public interface IObjectionPresetAsync
    {
        /// <summary>
        /// Foled of Preset
        /// </summary>
        string Path { get; }
        /// <summary>
        /// Id of loading object
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Asynchronously load preset
        /// </summary>
        /// <returns>Loaded Animation Object</returns>
        Task<IObjectionObject> LoadAsync();
    }
}
