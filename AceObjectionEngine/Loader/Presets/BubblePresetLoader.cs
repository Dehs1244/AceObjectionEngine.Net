using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Engine.Enums.SafetyEnums;
using AceObjectionEngine.Engine.Model.Layout;
using AceObjectionEngine.Engine.Model.Settings;
using AceObjectionEngine.Engine.Model;

namespace AceObjectionEngine.Loader.Presets
{
    public class BubblePresetLoader : BasePresetLoader<Bubble>
    {
        public BubblePresetLoader(int id) : base(id)
        {
        }

        public BubblePresetLoader()
        {
        }

        public override PresetPaths PresetPath => PresetPaths.Bubbles;

        public override Bubble LoadObject()
        {
            var imagePath = GetCombinedPresetPath();
            var name = GetFirstNameOfPreset();
            return new Bubble(new BubbleSettings()
            {
                Name = name,
                ImagePath = System.IO.Path.Combine(imagePath, $"{name}.png"),
                Audio = AudioSource.FromFile(System.IO.Path.Combine(imagePath, name, "audio", "audio.mp3"))
            });
        }
    }
}
