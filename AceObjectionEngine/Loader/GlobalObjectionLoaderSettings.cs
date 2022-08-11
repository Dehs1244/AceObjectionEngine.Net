using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Loader.Assets;
using AceObjectionEngine.Loader.Presets;

namespace AceObjectionEngine.Loader
{
    public static class GlobalObjectionLoaderSettings
    {
        private static AssetsSettings _assets = AssetsSettings.Default;
        private static PresetsSettings _presets = PresetsSettings.Default;

        public static AssetsSettings CurrentAssets => _assets ?? AssetsSettings.Default;
        public static PresetsSettings CurrentPresets => _presets ?? PresetsSettings.Default;

        static GlobalObjectionLoaderSettings()
        {
            //CurrentAssets.Use();
            //CurrentPresets.Use();
        }

        public static void ConfigureAssets(Action<AssetsSettings> config)
        {
            config?.Invoke(_assets);
            CurrentAssets.Use();
        }

        public static void ConfigureAssets(AssetsSettings settings)
        {
            _assets = settings ?? throw new ArgumentNullException(nameof(settings));
            _assets.Use();
        }

        public static async Task ConfigureAssetsAsync(Func<AssetsSettings, Task> config)
        {
            await config?.Invoke(_assets);
            await CurrentAssets.UseAsync();
        }

        public static async Task ConfigureAssetsAsync(AssetsSettings settings)
        {
            _assets = settings ?? throw new ArgumentNullException(nameof(settings));
            await _assets.UseAsync();
        }

        public static void ConfigurePresets(Action<PresetsSettings> config)
        {
            config?.Invoke(_presets);
            _presets.Use();
        }

        public static void ConfigurePresets(PresetsSettings settings)
        {
            _presets = settings ?? throw new ArgumentNullException(nameof(settings));
            _presets.Use();
        }

        public static async Task ConfigurePresetsAsync(Func<PresetsSettings, Task> config)
        {
            await config?.Invoke(_presets);
            await _presets.UseAsync();
        }

        public static async Task ConfigurePresetsAsync(PresetsSettings settings)
        {
            _presets = settings ?? throw new ArgumentNullException(nameof(settings));
            await _presets.UseAsync();
        }
    }
}
