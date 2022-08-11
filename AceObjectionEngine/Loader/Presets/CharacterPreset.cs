using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Engine.Enums.SafetyEnums;
using AceObjectionEngine.Engine.Model.Layout;
using AceObjectionEngine.Engine.Model.Settings;
using AceObjectionEngine.Abstractions.Layout.Character;
using AceObjectionEngine.Engine.Model;

namespace AceObjectionEngine.Loader.Presets
{
    public class CharacterPreset : BasePresetLoader<Character>
    {
        public override PresetPaths PresetPath => PresetPaths.Characters;

        public CharacterPreset(int id) : base(id)
        {
        }

        public CharacterPreset()
        {
        }

        public override Character LoadObject()
        {
            var meta = LoadMeta();
            var allPoses = StorageProvider.EnumerateAllRoutes(GetCombinedPresetPath("poses"));
            List<ICharacterPose> characterPoses = new List<ICharacterPose>();

            foreach (var pose in allPoses)
            {
                var metaPose = LoadMeta(pose + "/meta.json");
                characterPoses.Add(new CharacterPose(new CharacterPoseSettings(metaPose.ToJson())));
            }
            var settings = new CharacterSettings(meta.ToJson())
            {
                Poses = characterPoses
            };

            var character = new Character(settings);
            return character;
        }
    }
}
