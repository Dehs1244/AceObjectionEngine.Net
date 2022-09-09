using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Abstractions.Layout.Character;
using AceObjectionEngine.Abstractions;
using AceObjectionEngine.Engine.Enums;
using AceObjectionEngine.Engine.Enums.SafetyEnums;
using AceObjectionEngine.Loader.Presets;
using AceObjectionEngine.Engine.Model.Components;
using AceObjectionEngine.Loader.Utils;
using AceObjectionEngine.Engine.Infrastructure;
using AceObjectionEngine.Engine.Model;

namespace AceObjectionEngine.Settings
{
    public class CharacterSettings : BaseSettings<Character>
    {
        public string Name { get; set; }
        public string NamePlate { get; set; }
        public int? PoseId { get; set; }
        public BlipSexType? Sex { get; set; }
        public List<AceComponentSpan<ICharacterPose>> Poses { get; set; } = new List<AceComponentSpan<ICharacterPose>>();
        public CharacterLocations Side { get; set; }

        public CharacterSettings()
        {
        }

        public CharacterSettings(int id)
        {
            Id = id;
        }

        public CharacterSettings(AssetJson fromJson) : base(fromJson)
        {
        }

        public override void FromJson(AssetJson json)
        {
            Id = json["id"];
            Name = json["name"];
            NamePlate = json["namePlate"];
            Sex = json["sex"] == "female" ? BlipSexType.Female : BlipSexType.Male;
            switch (json["side"].ToString())
            {
                case "defense":
                    Side = CharacterLocations.Defense;
                    break;
                case "prosecution":
                    Side = CharacterLocations.Prosecution;
                    break;
                case "counsel":
                    Side = CharacterLocations.Counsel;
                    break;
                case "judge":
                    Side = CharacterLocations.Judge;
                    break;
                case "witness":
                    Side = CharacterLocations.Witness;
                    break;
                case "gallery":
                    Side = CharacterLocations.Gallery;
                    break;
            }

            if (json.ContainsKey("poses"))
            {
                foreach (var assetPoses in json["poses"])
                {
                    var settings = new CharacterPoseSettings(assetPoses);
                    Poses.Add(new AceComponentSpan<ICharacterPose>()
                    {
                        Component = new Lazy<ICharacterPose>(() => new CharacterPose(new CharacterPoseSettings(assetPoses))),
                        Id = settings.Id,
                        Name = settings.Name
                    });
                }
            }

        }
    }
}
