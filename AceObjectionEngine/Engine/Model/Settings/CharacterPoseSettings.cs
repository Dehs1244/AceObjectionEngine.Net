using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Abstractions;
using AceObjectionEngine.Engine.Infrastructure;
using AceObjectionEngine.Engine.Model.Layout;
using AceObjectionEngine.Exceptions;
using AceObjectionEngine.Loader.Assets;
using AceObjectionEngine.Loader.Presets;
using AceObjectionEngine.Loader.Utils;
using AceObjectionEngine.Engine.Animator;

namespace AceObjectionEngine.Engine.Model.Settings
{
    public class CharacterPoseSettings : BaseSettings<CharacterPose>
    {
        public string Name { get; set; }
        public int CharacterId { get; set; }
        public bool IsSpeedLines { get; set; }

        public string IdleImagePath { get; set; }
        public string SpeakImagePath { get; set; }
        public Audio[] AudioTicks { get; set; }
        public IRenderBranch[] PoseStates { get; set; } = Array.Empty<IRenderBranch>();

        public CharacterPoseSettings(AssetJson json) : base(json)
        {
        }

        public CharacterPoseSettings(int id) : base(id)
        {

        }

        public override void FromJson(AssetJson json)
        {
            Id = json["id"];
            Name = json["name"];
            if (json.ContainsKey("idleImageUrl")) throw new ObjectionNotLoadedException(this);
            IdleImagePath = json["idleImagePath"];
            if (json.ContainsKey("speakImageUrl")) throw new ObjectionNotLoadedException(this);
            if (json.ContainsKey("speakImagePath")) SpeakImagePath = json["speakImagePath"];
            else SpeakImagePath = IdleImagePath;
            if (json.ContainsKey("isSpeedlines")) IsSpeedLines = true;

            List<Audio> audioTicks = new List<Audio>();
            foreach(var assetAudio in json["audioTicks"])
            {
                if (assetAudio.ContainsKey("fileName")) throw new ObjectionNotLoadedException(this);
                audioTicks.Add(new Audio(new SoundSettings()
                {
                    AudioPath = assetAudio["filePath"],
                    Name = assetAudio["name"],
                    Volume = assetAudio["volume"],
                    StartPlay = TimeSpan.FromMilliseconds(Frame.SamplingFromObjectionLolTime((double)assetAudio["time"]))
                }));
            }
            var poseState = new List<IRenderBranch>();

            foreach(var assetPoseState in json["states"])
            {
                if(assetPoseState.ContainsKey("imageUrl")) throw new ObjectionNotLoadedException(this);
                var state = Sprite.FromFile(assetPoseState["fileName"]);
                poseState.Add(new PoseActions.SimplePoseAction()
                {
                    State = state,
                    Delay = TimeSpan.FromMilliseconds(assetPoseState["nextPoseDelay"])
                });
            }

            AudioTicks = audioTicks.ToArray();
            PoseStates = poseState.ToArray();
        }
    }
}
