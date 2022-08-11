using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Engine.Model.Layout;
using AceObjectionEngine.Engine.Model.Settings;
using AceObjectionEngine.Loader.Model;
using AceObjectionEngine.Loader.Utils;
using Newtonsoft.Json.Linq;
using AceObjectionEngine.Engine.Enums.SafetyEnums;

namespace AceObjectionEngine.Loader.Assets
{
    public sealed class BubbleLoader : BaseAssetLoader<Bubble>
    {
        public BubbleLoader(int id) : base(id)
        {
        }

        public BubbleLoader()
        {
        }

        private AssetJson LoadBubblesAsset() =>
            new AssetJson(JToken.Parse(
                File.ReadAllText($"{GlobalObjectionLoaderSettings.CurrentAssets.AssetsFolder}/{GlobalObjectionLoaderSettings.CurrentAssets.BubblesJsonFile}.json")
                ));

        public override LoaderData<Bubble> Request()
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<LoaderData<Bubble>> RequestAll()
        {
            var allCharacters = LoadBubblesAsset();
            foreach (var json in allCharacters)
            {
                Id = json["id"];
                using (var client = new WebClient())
                {
                    var path = $"{PlaceAssetFileToPresetPath(json["name"], PresetPaths.Bubbles)}.png";
                    var audioPath = $"{DeepFolderInPresetPath(PlaceAssetFileToPresetPath(json["name"], PresetPaths.Bubbles), "audio")}/audio.mp3";
                    client.DownloadFile($"https://objection.lol/Images/Bubbles/{Id}.png", path);
                    json.Add("imagePath", path);
                    client.DownloadFile($"https://objection.lol/Audio/Vocal/{Id}/1.mp3", audioPath);
                    json.Add("audioPath", audioPath);
                }
                yield return new LoaderData<Bubble>() { Subject = new Bubble(new BubbleSettings(json)), Successfully = true };
            }
        }
    }
}
