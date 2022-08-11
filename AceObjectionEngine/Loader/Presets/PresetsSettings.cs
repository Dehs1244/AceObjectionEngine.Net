using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Abstractions;
using AceObjectionEngine.Engine.Infrastructure;

namespace AceObjectionEngine.Loader.Presets
{
    public class PresetsSettings : IObjectionEngineSettings, ICloneable
    {
        public string PresetsFolder { get; set; } = string.Empty;
        public IStorageProvider StorageProvider { get; set; }

        public static PresetsSettings Default => new PresetsSettings() 
        { 
            PresetsFolder = "AceObjectionLib",
            StorageProvider = new LocalStorageProvider()
        };

        public object Clone() => (PresetsSettings)MemberwiseClone();

        public void Use()
        {
            if (!Directory.Exists(PresetsFolder)) Directory.CreateDirectory(PresetsFolder);
        }

        public async Task UseAsync() => await Task.Run(Use); 
    }
}
