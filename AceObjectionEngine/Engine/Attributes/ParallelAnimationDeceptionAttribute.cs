using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Engine.Attributes
{
    /// <summary>
    /// Organizes that the rendering of this type object will depend on checking the condition of this 
    /// </summary>
    /// <remarks>Advice: Use <seealso cref="NonRenderAttribute"/></remarks>
    public class ParallelAnimationDeceptionAttribute : Attribute
    {
        public Type[] Misleaders { get; }

        public ParallelAnimationDeceptionAttribute(params Type[] missleaders)
        {
            Misleaders = missleaders;
        }
    }
}
