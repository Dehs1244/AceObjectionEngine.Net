using AceObjectionEngine.Engine.Enums.SafetyEnums;
using AceObjectionEngine.Engine.Model.Components;
using AceObjectionEngine.Settings;
using AceObjectionEngine.Loader.Model;
using AceObjectionEngine.Loader.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Loader.Assets
{
    public class BackgroundLoader : ApiAsset<Background>
    {
        protected override Uri Url => new Uri("https://api.objection.lol/assets/background/getall");

        public BackgroundLoader(int id) : base(id)
        {
        }

        public BackgroundLoader()
        {
        }

        public override IEnumerable<Background> WrapCollection(AssetJson asset)
        {
            var oldId = Id;
            foreach (var assetSample in asset)
            {
                Id = assetSample["id"];

                if (IsAssetPathExists(PresetPaths.Characters))
                {
                    if (!GlobalObjectionLoaderSettings.CurrentAssets.Overwrite) continue;
                    else StorageProvider.RemoveRoute(GetAssetDownloadContainer(PresetPaths.Backgrounds));
                }

                Uri objectionHost = new Uri("https://objection.lol");
                Uri imageUri = new Uri(objectionHost, assetSample["url"].ToString());
                assetSample.RemoveByKey("url");

                assetSample.Add("imagePath", PlaceAssetImageToPresetPath(assetSample["name"].ToString(), PresetPaths.Backgrounds));
                if (!assetSample["deskUrl"].IsNullOrEmpty)
                {
                    Uri deskUri = new Uri(objectionHost, assetSample["deskUrl"].ToString());
                    GetImage(deskUri, PlaceAssetImageToPresetPath("desk", PresetPaths.Backgrounds));
                    assetSample.Add("imageDeskPath", PlaceAssetImageToPresetPath("desk", PresetPaths.Backgrounds));
                }
                if(assetSample.ContainsKey("deskUrl")) assetSample.RemoveByKey("deskUrl");

                PresetMetaData meta = new PresetMetaData(Id, assetSample["name"]);
                assetSample.CopyToDictionary(meta, "deskUrl");

                GetImage(imageUri, PlaceAssetImageToPresetPath(assetSample["name"].ToString(), PresetPaths.Backgrounds));
                SaveOrCreateMetaData(meta, PresetPaths.Backgrounds);
                BackgroundSettings settings = new BackgroundSettings(assetSample);

                yield return new Background(settings);
            }
            Id = oldId;
        }

        public override LoaderData<Background> Request()
        {
            var collection = RequestAll();
            return collection.Where(x => x.Subject.Id == Id).First();
        }
    }
}
