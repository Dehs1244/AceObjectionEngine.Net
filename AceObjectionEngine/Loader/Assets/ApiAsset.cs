using AceObjectionEngine.Loader.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Exceptions;
using AceObjectionEngine.Abstractions;
using AceObjectionEngine.Loader.Utils;
using AceObjectionEngine.Engine.Enums.SafetyEnums;
using System.IO;

namespace AceObjectionEngine.Loader.Assets
{
    public abstract class ApiAsset<T> : BaseAssetLoader<T> where T : IObjectionObject
    {
        protected abstract Uri Url { get; }
        protected Uri ObjectionHost => new Uri("https://objection.lol");
        public string Name;

        public ApiAsset(int id) : base(id)
        {
        }

        public ApiAsset()
        {
        }

        public abstract IEnumerable<T> WrapCollection(AssetJson asset);

        public override IEnumerable<LoaderData<T>> RequestAll() => 
            WrapCollection(Get(Url)).Select(x => new LoaderData<T>() { Subject = x, Successfully = true });

        protected AssetJson Get(Uri url)
        {
            using (var client = new WebClient())
            {
                var loadedData = client.DownloadString(url);
                try
                {
                    var json = new AssetJson(JToken.Parse(loadedData));
                    return json;
                }
                catch
                {
                    throw new ObjectionJsonException(loadedData);
                }
            }
        }

        protected void GetImage(Uri url, string fileName) => GetFile(url, fileName, "png");
        protected void GetAudio(Uri url, string fileName) => GetFile(url, fileName, "mp3");

        protected string PlaceAssetImageToPresetPath(string fileName, PresetPaths path) => PlaceAssetFileToPresetPath(fileName, path, "png");
        protected string PlaceAssetAudioToPresetPath(string fileName, PresetPaths path) => PlaceAssetFileToPresetPath(fileName, path, "mp3");

        protected bool IsApiFileExists(Uri url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "HEAD";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            return response.ContentType != "text/html";
        }

        protected void GetFile(Uri url, string fileName, string extension)
        {
            using (var client = new WebClient())
            {
                if(Path.GetExtension(fileName) == null) client.DownloadFile(url, $"{fileName}.{extension}");
                else client.DownloadFile(url, fileName);
            }
        }
    }
}
