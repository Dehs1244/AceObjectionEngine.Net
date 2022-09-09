using AceObjectionEngine.Engine.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Engine.Model.Components;
using AceObjectionEngine.Loader.Utils;

namespace AceObjectionEngine.Settings
{
    public class DeskSettings : BaseSettings<Desk>
    {
        public string ImagePath { get; set; }

        public override void FromJson(AssetJson json)
        {
            ImagePath = json["imagePath"];
        }
    }
}
