using AceObjectionEngine.Engine.Enums;
using AceObjectionEngine.Engine.Model.Components;
using AceObjectionEngine.Loader.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Engine.Enums.SafetyEnums;
using AceObjectionEngine.Loader.Presets;
using System.Collections.ObjectModel;
using System.Net;
using AceObjectionEngine.Exceptions;
using System.IO;

namespace AceObjectionEngine.Loader.Assets
{
    public class BlipAudioAsset : BaseAssetLoader<Audio>
    {
        public BlipAudioAsset()
        {

        }

        public BlipAudioAsset(int id) : base(id)
        {

        }

        public override LoaderData<Audio> Request()
        {
            var allData = RequestAll().ToList();
            if (Id == -1) throw new ObjectionIdLoaderException<Audio>(this);
            return allData.Where(x => x.Subject.Id == Id).First();
        }

        public override IEnumerable<LoaderData<Audio>> RequestAll()
        {
            Id = 0;
            if(IsAssetPathExists(PresetPaths.BlipAudio) && GlobalObjectionLoaderSettings.CurrentAssets.Overwrite)
                StorageProvider.RemoveRoute(GetAssetDownloadContainer(PresetPaths.BlipAudio, true));

            if (!IsAssetPathExists(PresetPaths.BlipAudio))
            {
                var blipPath = $"{GetAssetDownloadContainer(PresetPaths.BlipAudio, true)}/blip.wav";

                using (var client = new WebClient())
                {
                    client.DownloadFile($"https://objection.lol/Audio/blip.wav", blipPath);
                }
                yield return new LoaderData<Audio>() { Subject = new BlipAudioPreset(BlipSexType.Male).LoadObject(), Successfully = true };
            }

            Id = 1;

            if (IsAssetPathExists(PresetPaths.BlipAudio) && GlobalObjectionLoaderSettings.CurrentAssets.Overwrite)
                StorageProvider.RemoveRoute(GetAssetDownloadContainer(PresetPaths.BlipAudio, true));

            if (!IsAssetPathExists(PresetPaths.BlipAudio))
            {
                var femaleBlipPath = $"{GetAssetDownloadContainer(PresetPaths.BlipAudio, true)}/blip-female.wav";
                using (var client = new WebClient())
                {
                    client.DownloadFile($"https://objection.lol/Audio/blip-female.wav", femaleBlipPath);
                }

                yield return new LoaderData<Audio>() { Subject = new BlipAudioPreset(BlipSexType.Female).LoadObject(), Successfully = true };
            }
        }
    }
}
