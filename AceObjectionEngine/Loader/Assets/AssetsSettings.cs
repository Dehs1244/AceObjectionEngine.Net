using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Abstractions;

namespace AceObjectionEngine.Loader.Assets
{
    public sealed class AssetsSettings : IObjectionEngineSettings, ICloneable
    {
        public string AssetsFolder { get; set; } = string.Empty;
        public string BubblesJsonFile { get; set; } = string.Empty;
        public string CharactersJsonFile { get; set; } = string.Empty;
        public bool Overwrite { get; set; }

        public static AssetsSettings Default => new AssetsSettings()
        {
            AssetsFolder = "Assets",
            BubblesJsonFile = "bubbles",
            CharactersJsonFile = "characters",
            Overwrite = true
        };

        public object Clone() => (AssetsSettings)MemberwiseClone();

        public void Use()
        {
            if (!Directory.Exists(AssetsFolder)) Directory.CreateDirectory(AssetsFolder);
        }

        public async Task UseAsync() => await Task.Run(Use);
    }
}
