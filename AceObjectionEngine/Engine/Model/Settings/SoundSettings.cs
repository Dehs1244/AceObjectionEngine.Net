using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Abstractions;
using AceObjectionEngine.Loader.Presets;
using AceObjectionEngine.Engine.Model.Components;
using AceObjectionEngine.Loader.Utils;
using AceObjectionEngine.Engine.Attributes;
using AceObjectionEngine.Exceptions;
using AceObjectionEngine.Engine.Utils;
using AceObjectionEngine.Engine.Infrastructure;

namespace AceObjectionEngine.Settings
{
    public class SoundSettings : BaseSettings<Audio>
    {
        public string Name { get; set; }
        [ObjectionOptionRange(1.0, regulation: true)]
        public float Volume { get; set; }
        public string AudioPath { get; set; }
        public TimeSpan StartPlay { get; set; }
        public bool? IsFixate { get; set; }

        public SoundSettings()
        {
        }

        public SoundSettings(int id) : base(id)
        {
        }

        public SoundSettings(AssetJson json) : base(json)
        {
        }

        public override void FromJson(AssetJson json)
        {
            Id = json["id"];
            Name = json["name"];
            Volume = json["volume"];
            if (json.ContainsKey("url")) throw new ObjectionNotLoadedException(this);
            AudioPath = json["audioPath"];
        }
    }
}
