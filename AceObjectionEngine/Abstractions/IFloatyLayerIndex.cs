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
        SingleEntry<int> LayerIndexer { get; set; }

        Type[] DependencyIndexer { get; }
    }
}
