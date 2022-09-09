using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Engine.Enums.SafetyEnums;
using AceObjectionEngine.Loader.Model;
using AceObjectionEngine.Engine.Model.Components;
using AceObjectionEngine.Settings;
using AceObjectionEngine.Exceptions;
using AceObjectionEngine.Loader.Utils;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Threading;
using Newtonsoft.Json;
using System.IO;

namespace AceObjectionEngine.Loader.Assets
{
    public sealed class CharacterLoader : BaseAssetLoader<Character>
    {
        public CharacterLoader()
        {

        }

        public CharacterLoader(int id) : base(id)
        {
        }

        private AssetJson LoadCharactersAsset() =>
            new AssetJson(JToken.Parse(
                StorageProvider.ReadPointContent($"{GlobalObjectionLoaderSettings.CurrentAssets.AssetsFolder}/{GlobalObjectionLoaderSettings.CurrentAssets.CharactersJsonFile}.json")
                ));

        private bool _IsCharacterFemale()
        {
            var femaleIds = new int[] { 4, 7, 14, 16, 18, 19, 22, 24, 26, 30, 32, 34, 37, 40, 46, 48, 50, 53, 55, 56, 59, 62, 65, 68, 69, 71, 72, 77, 82, 84, 85, 91, 92, 93, 99, 100 };
            return femaleIds.Any(x => x == Id);
        }

        public override LoaderData<Character> Request()
        {
            var allData = RequestAll();
            if (Id == -1) throw new ObjectionIdLoaderException<Character>(this);
            return allData.Where(x => x.Subject.Id == Id).First();
        }

        public override IEnumerable<LoaderData<Character>> RequestAll()
        {
            var allCharacters = LoadCharactersAsset();
            foreach(var json in allCharacters)
            {
                Id = json["id"];
                if (IsAssetPathExists(PresetPaths.Characters))
                {
                    if (!GlobalObjectionLoaderSettings.CurrentAssets.Overwrite) continue;
                    else StorageProvider.RemoveRoute(GetAssetDownloadContainer(PresetPaths.Characters));
                }

                Thread.Sleep(3000);
                PresetMetaData meta = new PresetMetaData(Id, json["name"]);
                if (_IsCharacterFemale()) json.Add("sex", "female");
                else json.Add("sex", "male");

                json.CopyToDictionary(meta, "poses");

                foreach (var assetPoseJson in json["poses"])
                {
                    using (var client = new WebClient())
                    {
                        bool isDownloadSpeak = !string.IsNullOrEmpty(assetPoseJson["speakImageUrl"]) &&
                            assetPoseJson["idleImageUrl"] != assetPoseJson["speakImageUrl"];
                        var metaPose = new PresetMetaData(assetPoseJson["id"], assetPoseJson["name"]);

                        var idlePath = _DownloadPoseToPath(assetPoseJson["name"], assetPoseJson["idleImageUrl"], true);
                        assetPoseJson.RemoveByKey("idleImageUrl");

                        assetPoseJson.Add("idleImagePath", idlePath);

                        if (isDownloadSpeak)
                        {
                            var speakPath = _DownloadPoseToPath(assetPoseJson["name"], assetPoseJson["speakImageUrl"], false);
                            assetPoseJson.Add("speakImagePath", speakPath);
                        }
                        if (assetPoseJson.ContainsKey("speakImageUrl")) assetPoseJson.RemoveByKey("speakImageUrl");
                        var posePath = DeepFolderInPresetPath(PlaceAssetFolderToPresetPath("poses", PresetPaths.Characters), assetPoseJson["name"]);

                        if (assetPoseJson.ContainsKey("audioTicks") && !assetPoseJson["audioTicks"].IsEmptyArray)
                        {
                            foreach(var assetAudioTickJson in assetPoseJson["audioTicks"])
                            {
                                var audioFileName = $"{assetAudioTickJson["fileName"]}.mp3";
                                if (!string.IsNullOrEmpty(Path.GetExtension(assetAudioTickJson["fileName"])))
                                    audioFileName = assetAudioTickJson["fileName"];

                                var audioPosePath = DeepInPresetPath(posePath, true, $"audioTicks", audioFileName);
                                client.DownloadFile($"https://objection.lol/Audio/{audioFileName}", audioPosePath);
                                assetAudioTickJson.Add("name", assetAudioTickJson["fileName"].ToString());
                                assetAudioTickJson.Add("filePath", audioPosePath);
                                assetAudioTickJson.RemoveByKey("fileName");
                            }
                        }

                        if (assetPoseJson.ContainsKey("states") && !assetPoseJson["states"].IsEmptyArray)
                        {
                            foreach (var assetStatePose in assetPoseJson["states"])
                            {
                                var stateFileName = $"{assetStatePose["imageUrl"]}.gif";

                                var stateFilePosePath = DeepInPresetPath(posePath, true, $"states", stateFileName);
                                client.DownloadFile($"https://objection.lol/Images/Characters/{Id}/{stateFileName}", stateFilePosePath);
                                assetStatePose.Add("fileName", stateFilePosePath);
                                assetStatePose.RemoveByKey("imageUrl");
                            }
                        }

                        //TODO: FUNCTION TICKS PROCESSOR
                        assetPoseJson.CopyToDictionary(metaPose, "functionTicks");

                        var metaPosePath = Path.Combine(posePath, "meta.json");
                        if (!StorageProvider.IsPointExists(metaPosePath))
                        {
                            StorageProvider.CreatePoint(metaPosePath);
                            StorageProvider.WriteContentInPoint(metaPosePath, JsonConvert.SerializeObject(metaPose));
                        }
                    }
                }
                SaveOrCreateMetaData(meta, PresetPaths.Characters);

                yield return new LoaderData<Character>() { Subject = new Character(new CharacterSettings(json)), Successfully = true };
            }
        }

        private string _DownloadPoseToPath(string name, string imageName, bool isIdle)
        {
            using (var client = new WebClient())
            {
                var posePath = DeepFolderInPresetPath(PlaceAssetFolderToPresetPath("poses", PresetPaths.Characters), name, false);
                var imageFilePath = Path.Combine(posePath, isIdle ? "idle.gif" : "animation.gif");

                client.DownloadFile($"https://objection.lol/Images/Characters/{Id}/{imageName}.gif", imageFilePath);
                return imageFilePath;
            }
        }
    }
}
