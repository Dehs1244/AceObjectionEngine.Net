using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Abstractions;

namespace AceObjectionEngine.Exceptions
{
    public class ObjectionNotLoadedException : ObjectionException
    {
        public ObjectionNotLoadedException(IObjectionPreset preset) : base($"Preset {nameof(preset)} detect not loaded data, while converting from json")
        {

        }

        public ObjectionNotLoadedException(IObjectionSettings settings) : base($"Settings {nameof(settings)} detect not loaded data, while converting from json")
        {

        }
    }
}
