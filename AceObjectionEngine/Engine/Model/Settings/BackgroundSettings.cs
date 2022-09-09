using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Abstractions;
using AceObjectionEngine.Engine.Model.Components;
using AceObjectionEngine.Loader.Presets;
using AceObjectionEngine.Loader.Utils;
using AceObjectionEngine.Exceptions;
using AceObjectionEngine.Engine.Infrastructure;

namespace AceObjectionEngine.Settings
{
    public class BackgroundSettings : BaseSettings<Background>
    {
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public string ImageDeskPath { get; set; }
        public bool IsWide { get; set; }

        public BackgroundSettings()
        {
        }

        public BackgroundSettings(AssetJson json) : base(json)
        {
        }

        public override void FromJson(AssetJson json)
        {
            Id = json["id"];
            Name = json["name"];
            if (json.ContainsKey("url")) throw new ObjectionNotLoadedException(this);
            ImagePath = json["imagePath"];
            if (json.ContainsKey("deskUrl")) throw new ObjectionNotLoadedException(this);
            if (json.ContainsKey("imageDeskPath")) ImageDeskPath = json["imageDeskPath"];
            else ImageDeskPath = null;
            IsWide = json["isWide"];
        }
    }
}
