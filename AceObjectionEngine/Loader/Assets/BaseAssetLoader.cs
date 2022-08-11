using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Abstractions;
using AceObjectionEngine.Loader.Model;
using AceObjectionEngine.Exceptions;
using AceObjectionEngine.Engine.Enums.SafetyEnums;
using Newtonsoft.Json;
using System.IO;

namespace AceObjectionEngine.Loader.Assets
{
    public abstract class BaseAssetLoader<T> : IObjectionAsset<T> where T : IObjectionObject
    {
        public bool Exists => _loaded;
        public int Id { get; set; }
        public IStorageProvider StorageProvider => GlobalObjectionLoaderSettings.CurrentPresets.StorageProvider;

        public IEnumerable<T> Loaded => new List<T>();

        protected bool _loaded;

        public abstract LoaderData<T> Request();

        public abstract IEnumerable<LoaderData<T>> RequestAll();

        public BaseAssetLoader(int id)
        {
            GlobalObjectionLoaderSettings.CurrentAssets.Use();
            Id = id;
        }

        public BaseAssetLoader()
        {
            GlobalObjectionLoaderSettings.CurrentAssets.Use();
            Id = -1;
        }

        public T Load()
        {
            var data = Request();
            if (!data.Successfully) throw new ObjectionLoaderException<T>(this);
            ((List<T>)Loaded).Add(data.Subject);
            return data.Subject;
        }

        public IEnumerable<T> LoadAll()
        {
            List<LoaderData<T>> data = RequestAll().ToList();
            if (data.Any() && data.Any(x=> !x.Successfully)) throw new ObjectionLoaderException<T>(this);
            ((List<T>)Loaded).AddRange(data.Select(x => x.Subject));
            _loaded = true;
            return data.Select(x=> x.Subject);
        }

        protected void SaveOrCreateMetaData(PresetMetaData meta, PresetPaths preset)
        {
            var metaPath = GetAssetDownloadContainer(preset) + "/meta.json";
            if (!StorageProvider.IsPointExists(metaPath)) StorageProvider.CreatePoint(metaPath);
            StorageProvider.WriteContentInPoint(metaPath, JsonConvert.SerializeObject(meta));
        }

        protected string GetAssetDownloadContainer(PresetPaths path, bool create = false)
        {
            var presetPath = Path.Combine(GlobalObjectionLoaderSettings.CurrentPresets.PresetsFolder, path.ToString());
            if (!StorageProvider.IsRouteExists(presetPath))
            {
                if (!create) return null;
                StorageProvider.CreateRoute(presetPath);
            }

            var placedAssetPath = Path.Combine(presetPath, Id.ToString());
            if (!StorageProvider.IsRouteExists(placedAssetPath))
            {
                if (!create) return null;
                StorageProvider.CreateRoute(placedAssetPath);
            }
            return placedAssetPath;
        }

        protected string PlaceAssetFileToPresetPath(string fileName, PresetPaths path, string extension = null)
        {
            if (extension != null) extension = $".{extension}";
            fileName = fileName
                .Replace("'", "")
                .Replace(":", "");
            var placedAssetPath = Path.Combine(GetAssetDownloadContainer(path, true), fileName) + extension;
            return placedAssetPath;
        }

        protected string PlaceAssetFolderToPresetPath(string folder, PresetPaths path)
        {
            var placedAssetPath = Path.Combine(GetAssetDownloadContainer(path, true), folder);
            if (!StorageProvider.IsRouteExists(placedAssetPath)) StorageProvider.CreateRoute(placedAssetPath);
            return placedAssetPath;
        }

        protected bool IsAssetPathExists(PresetPaths path) => !string.IsNullOrEmpty(GetAssetDownloadContainer(path));

        protected string DeepInPresetPath(string placedPath, bool isFileRoute = false, params string[] deepName)
        {
            var result = placedPath;
            foreach(var name in deepName)
            {
                result = DeepFolderInPresetPath(result, name, isFileRoute);
            }
            return result;
        }

        protected string DeepFolderInPresetPath(string placedPath, string name, bool isFileRoute = false)
        {
            var deepedPresetPath = Path.Combine(placedPath, name);
            var shatterPaths = StorageProvider.SeparateRoute(deepedPresetPath);
            string result = string.Empty;
            for(var i = 0; i < shatterPaths.Count(); i++)
            {
                var path = shatterPaths.ElementAt(i);
                var routePath = path;
                if (isFileRoute && i == shatterPaths.Count() - 1)
                {
                    result = Path.Combine(result, routePath);
                    continue;
                }

                routePath = routePath
                    .Replace(":", "");
                result = Path.Combine(result, routePath);
                if (!StorageProvider.IsRouteExists(result)) StorageProvider.CreateRoute(result);
            }
            return result;
        }

        public void DownloadAll()
        {
            LoadAll();
        }
    }
}
