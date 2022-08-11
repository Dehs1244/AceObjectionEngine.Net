using AceObjectionEngine.Engine.Enums.SafetyEnums;
using AceObjectionEngine.Engine.Model.Layout;
using AceObjectionEngine.Engine.Model.Settings;
using AceObjectionEngine.Loader.Model;
using AceObjectionEngine.Loader.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Loader.Assets
{
    public sealed class MusicLoader : ApiAsset<Audio>
    {
        protected override Uri Url => new Uri("https://api.objection.lol/assets/music/getall");

        public MusicLoader(int id) : base(id)
        {
        }

        public MusicLoader()
        {
        }

        public override LoaderData<Audio> Request()
        {
            var allMusic = RequestAll();
            return allMusic.Where(x => x.Subject.Id == Id).First();
        }

        public override IEnumerable<Audio> WrapCollection(AssetJson asset)
        {
            foreach (var assetSample in asset)
            {
                Id = assetSample["id"];

                if (IsAssetPathExists(PresetPaths.Audio))
                {
                    if (!GlobalObjectionLoaderSettings.CurrentAssets.Overwrite) continue;
                    else StorageProvider.RemoveRoute(GetAssetDownloadContainer(PresetPaths.Audio));
                }

                Uri audioUri = new Uri(ObjectionHost, assetSample["url"].ToString());
                if (!IsApiFileExists(audioUri)) continue;

                var name = assetSample["name"].ToString();
                var meta = new PresetMetaData(Id, name);
                GetAudio(audioUri, PlaceAssetAudioToPresetPath(name, PresetPaths.Audio));
                assetSample.RemoveByKey("url");

                assetSample.Add("audioPath", PlaceAssetAudioToPresetPath(name, Engine.Enums.SafetyEnums.PresetPaths.Audio));
                assetSample.CopyToDictionary(meta);
                SaveOrCreateMetaData(meta, PresetPaths.Audio);
                var audioSettings = new SoundSettings(assetSample);
                yield return new Audio(audioSettings);
            }
        }
    }
}
