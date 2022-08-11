using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AceObjectionEngine.Engine.Enums.SafetyEnums
{
    public class PresetPaths : SafetyEnum<PresetPaths>
    {
        public static PresetPaths Main => RegisterValue("Presets");
        public static PresetPaths Audio => RegisterValue(Path.Combine(Main.ToString(), "Audio"));
        public static PresetPaths BlipAudio => RegisterValue(Path.Combine(Main.ToString(), "BlipAudio"));
        public static PresetPaths Backgrounds => RegisterValue(Path.Combine(Main.ToString(), "Backgrounds"));
        public static PresetPaths Characters => RegisterValue(Path.Combine(Main.ToString(), "Characters"));
        public static PresetPaths CharactersPose => RegisterValue(Path.Combine(Characters.ToString(), "Poses"));
        public static PresetPaths Music => RegisterValue(Path.Combine(Main.ToString(), "Music"));
        public static PresetPaths Bubbles => RegisterValue(Path.Combine(Main.ToString(), "Bubbles"));
    }
}
