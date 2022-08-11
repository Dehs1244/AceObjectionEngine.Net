using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Abstractions.Async;

namespace AceObjectionEngine.Abstractions
{
    public interface IObjectionPreset : IObjectionPresetAsync
    {
        IObjectionObject Load();
    }
}
