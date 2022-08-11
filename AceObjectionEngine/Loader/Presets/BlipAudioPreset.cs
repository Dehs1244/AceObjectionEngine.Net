using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Engine.Model.Layout;
using AceObjectionEngine.Engine.Enums.SafetyEnums;
using AceObjectionEngine.Engine.Enums;
using AceObjectionEngine.Engine.Model.Settings;

namespace AceObjectionEngine.Loader.Presets
{
    public sealed class BlipAudioPreset : BasePresetLoader<Audio>
    {
        public BlipSexType Type { get; }

        public override PresetPaths PresetPath => PresetPaths.BlipAudio;

        public BlipAudioPreset(BlipSexType type) : base(-1)
        {
            Type = type;
        }

        public override Audio LoadObject()
        {
            var path = string.Empty;
            var name = string.Empty;
            switch (Type)
            {
                case BlipSexType.Female:
                    Id = 1;
                    path += $"{GetCombinedPresetPath()}/blip-female.wav";
                    name = "Female Blip";
                    break;
                case BlipSexType.Male:
                    Id = 0;
                    path += $"{GetCombinedPresetPath()}/blip.wav";
                    name = "Male Blip";
                    break;
            }
            var settings = new SoundSettings(Id)
            {
                AudioPath = path,
                Name = name,
                Volume = 0.7f
            };

            return new Audio(settings);
        }
    }
}
