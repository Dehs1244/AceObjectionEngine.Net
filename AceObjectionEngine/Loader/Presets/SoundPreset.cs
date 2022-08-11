using AceObjectionEngine.Engine.Enums.SafetyEnums;
using AceObjectionEngine.Engine.Model.Layout;
using AceObjectionEngine.Engine.Model.Settings;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Loader.Presets
{
    public class SoundPreset : BasePresetLoader<Audio>
    {
        public override PresetPaths PresetPath => PresetPaths.Audio;

        public SoundPreset(int id) : base(id)
        {
        }

        public SoundPreset()
        {
        }

        public virtual Audio LoadAudio()
        {
            var metaJson = LoadMeta();
            return new Audio(new SoundSettings(metaJson.ToJson()));
        }

        public override Audio LoadObject() => LoadAudio();
    }
}
