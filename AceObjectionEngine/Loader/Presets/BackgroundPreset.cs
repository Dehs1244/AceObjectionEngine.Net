using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Engine.Enums.SafetyEnums;
using AceObjectionEngine.Engine.Model.Layout;
using AceObjectionEngine.Loader.Presets;
using AceObjectionEngine.Engine.Model.Settings;

namespace AceObjectionEngine.Loader.Presets
{
    public class BackgroundPreset : BasePresetLoader<Background>
    {
        
        public override PresetPaths PresetPath => PresetPaths.Backgrounds;

        public BackgroundPreset(int id) : base(id)
        {
        }

        public BackgroundPreset()
        {
        }

        public override Background LoadObject()
        {
            var meta = LoadMeta();
            var background = new Background(new BackgroundSettings(meta.ToJson()));
            return background;
        }
    }
}
