using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Engine.Enums.SafetyEnums;
using AceObjectionEngine.Engine.Model.Components;
using AceObjectionEngine.Settings;
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
            List<AceComponentSpan<ICharacterPose>> characterPoses = new List<AceComponentSpan<ICharacterPose>>();

            foreach (var pose in allPoses)
            {
                var metaPose = LoadMeta(pose + "/meta.json");
                var poseSettings = new CharacterPoseSettings(metaPose.ToJson());
                characterPoses.Add(new AceComponentSpan<ICharacterPose>() 
                {
                    Component = new Lazy<ICharacterPose>(() => new CharacterPose(poseSettings)),
                    Id = poseSettings.Id,
                    Name = poseSettings.Name
                });
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
