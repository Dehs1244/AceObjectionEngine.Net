using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Engine.Attributes
{
    /// <summary>
    /// Attribute for secondary objects that should not appear more than once in layer
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class)]
    public class MinorObjectAttribute : Attribute
    {
    }
}
