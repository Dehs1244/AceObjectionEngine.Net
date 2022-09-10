using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Engine.Attributes;
using AceObjectionEngine.Engine.Infrastructure;

namespace AceObjectionEngine.Abstractions
{
    /// <summary>
    /// Designed for objects that are intended to be used on different layer indexes, indicated by <see cref="FloatyIndexInvoker">
    /// </summary>
    public interface IFloatyLayerIndex
    {
        /// <summary>
        /// Binding to an index
        /// </summary>
        SingleEntry<int> LayerIndexer { get; set; }

        /// <summary>
        /// Dependencies that the index will rely on
        /// </summary>
        Type[] DependencyIndexer { get; }
    }
}
