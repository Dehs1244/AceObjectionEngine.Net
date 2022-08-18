using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Engine.Attributes
{
    /// <summary>
    /// Quarantees that the object will be present in the complex rendering, but will not be processed itself
    /// Can be applied to objects that will be animated from externally and not by the renderer itself
    /// </summary>
    public class NonRenderAttribute : Attribute
    {
    }
}
