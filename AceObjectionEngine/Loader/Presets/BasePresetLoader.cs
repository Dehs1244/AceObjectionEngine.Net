using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Abstractions;
using AceObjectionEngine.Engine.Model;
using AceObjectionEngine.Engine.Enums.SafetyEnums;
using AceObjectionEngine.Loader.Model;
using AceObjectionEngine.Loader.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AceObjectionEngine.Loader.Presets
{
    public abstract class BasePresetLoader<T> : IObjectionPreset where T : IObjectionObject
    {
        public abstract PresetPaths PresetPath { get; }
        public string Path => PresetPath.ToString();
        public int Id { get; set; }
        public IStorageProvider StorageProvider => GlobalObjectionLoaderSettings.CurrentPresets.StorageProvider;

        public BasePresetLoader(int id)
        {
            Id = id;
        }

        public BasePresetLoader()
        {
            Id = -1;
        }

        public abstract T LoadObject();
        public async Task<T> LoadObjectAsync() => await new Task<T>(() => LoadObject());

        public IObjectionObject Load() => LoadObject();

        public async Task<IObjectionObject> LoadAsync() => await LoadObjectAsync();

        public IEnumerable<AceComponentSpan<T>> LoadAll()
        {
            var routes = StorageProvider.EnumerateAllRoutes(System.IO.Path.Combine(GlobalObjectionLoaderSettings.CurrentPresets.PresetsFolder, Path));
            ICollection<AceComponentSpan<T>> objects = new List<AceComponentSpan<T>>();
            foreach(var route in routes)
            {
                Id = int.Parse(new System.IO.DirectoryInfo(route).Name);
                var id = Id;
                objects.Add(new AceComponentSpan<T>()
                {
                    Component = new Lazy<T>(() =>
                    {
                        Id = id;
                        return LoadObject();
                    }),
                    Id = Id,
                    Name = LoadMeta().Name
                });
            }
            Id = 0;

            return objects;
        }

        protected void SaveMetaAsJsonFile(AssetJson json)
        {
            if (StorageProvider.IsPointExists(GetCombinedPresetPath("meta.json")))
                StorageProvider.CreatePoint(GetCombinedPresetPath("meta.json"));

            StorageProvider.WriteContentInPoint(GetCombinedPresetPath("meta.json"), json);
        }

        protected PresetMetaData LoadMeta(string path) => JsonConvert.DeserializeObject<PresetMetaData>(
            StorageProvider.ReadPointContent(path));

        protected PresetMetaData LoadMeta() => LoadMeta(GetCombinedPresetPath("meta.json"));

        protected string GetFirstNameOfPreset() => System.IO.Path.GetFileNameWithoutExtension(StorageProvider.EnumerateAllPoints(GetCombinedPresetPath()).First());

        protected string GetCombinedPresetPath(params string[] paths)
        {
            var presetPath = GetCombinedPresetPath();
            foreach(var path in paths)
            {
                presetPath = System.IO.Path.Combine(presetPath, path);
            }
            return presetPath;
        }

        protected string GetCombinedPresetPath()
        {
            var presetPath = System.IO.Path.Combine(GlobalObjectionLoaderSettings.CurrentPresets.PresetsFolder, Path);
            //TODO: THROW EXCEPTION, THAT PRESET IS NOT EXISTS
            if (!StorageProvider.IsRouteExists(presetPath)) StorageProvider.CreateRoute(presetPath);
            return System.IO.Path.Combine(presetPath, Id.ToString());
        }
    }
}
