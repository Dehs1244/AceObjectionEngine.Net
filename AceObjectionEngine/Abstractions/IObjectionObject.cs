using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Loader.Utils;

namespace AceObjectionEngine.Abstractions
{
    /// <summary>
    /// Objection object interface
    /// </summary>
    public interface IObjectionObject : IAnimationObject, IDisposable
    {
        /// <summary>
        /// Id of object
        /// </summary>
        int Id { get; }
    }
}
