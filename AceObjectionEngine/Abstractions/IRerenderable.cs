using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Abstractions
{
    /// <summary>
    /// An object implementation interface that forces the rendering system to render this object repeatedly
    /// </summary>
    public interface IRerenderable
    {
        /// <summary>
        /// Checks whether this object needs to be rendering again
        /// </summary>
        bool IsNeedReRender { get; set; }
    }
}
