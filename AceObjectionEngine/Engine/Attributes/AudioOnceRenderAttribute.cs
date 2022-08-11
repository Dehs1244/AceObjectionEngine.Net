using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Engine.Attributes
{
    /// <summary>
    /// Allows you to use one audio source file instead of creating multiple
    /// </summary>
    /// <remarks>Skips the general audio rendering which can be dangerous but optimizes the rendering and prevents re-copying of file</remarks>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class AudioOnceRenderAttribute : Attribute
    {
        public AudioOnceRenderAttribute()
        {

        }
    }
}
