using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Engine.Model.Layout;
using AceObjectionEngine.Loader.Presets;
using AceObjectionEngine.Loader.Utils;
using AceObjectionEngine.Engine.Infrastructure;
using AceObjectionEngine.Abstractions;
using AceObjectionEngine.Engine.Effects;
using AceObjectionEngine.Helpers;

namespace AceObjectionEngine.Engine.Model.Settings
{
    public class BubbleSettings : BaseSettings<Bubble>
    {
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public AudioSource Audio { get; set; }
        public AcePoint ShakeRange { get; set; } = new AcePoint(4f, 4f);
        public AceSize AnimatorSize { get; set; } = new AceSize(960, 640);

        private ICollection<IAnimationEffect> _effects = new List<IAnimationEffect>();
        public ICollection<IAnimationEffect> Effects { get => 
                _effects.Any() ? _effects : EnumerableHelper.ToEnumerable<IAnimationEffect>(new ShakeEffect(AnimatorSize, ShakeRange, Audio)).ToList(); 
            set => _effects = value; }

        public BubbleSettings()
        {
        }

        public BubbleSettings(AssetJson fromJson) : base(fromJson)
        {
        }

        public override void FromJson(AssetJson json)
        {
            Id = json["id"];
            Name = json["name"];
            ImagePath = json["imagePath"];
            Audio = AudioSource.FromFile(json["audioPath"]);
            Effects.Add(new ShakeEffect(AnimatorSize, ShakeRange, Audio));
        }
    }
}
