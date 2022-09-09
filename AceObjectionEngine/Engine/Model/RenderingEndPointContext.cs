using AceObjectionEngine.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Engine.Model
{
    public struct RenderingEndPointContext
    {
        public readonly IAudioMixer MainMixer;

        public RenderingEndPointContext(IAudioMixer mainMixer)
        {
            MainMixer = mainMixer;
        }
    }
}
