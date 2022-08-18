using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Abstractions.Async
{
    /// <summary>
    /// Interface for creating AceObjectionEngine settings
    /// </summary>
    public interface IObjectionEngineSettingsAsync
    {
        /// <summary>
        /// Asynchronously Called every time the setting is used
        /// </summary>
        /// <returns></returns>
        Task UseAsync();
    }
}
