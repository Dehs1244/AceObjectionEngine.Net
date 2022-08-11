using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Abstractions;
using AceObjectionEngine.Engine.Model.Layout;
using AceObjectionEngine.Loader.Utils;
using AceObjectionEngine.Engine.Enums;

namespace AceObjectionEngine.Engine.Model.Settings
{
    public class ChatBoxSettings : IObjectionSettings<ChatBox>
    {
        public int Id { get; }

        public int Duration => 0;

        public string Text { get; set; }
        public int FontSize { get; set; } = 24;
        public int Width { get; set; } = 960;
        public int Height { get; set; } = 640;
        public ChatBoxAlign Align { get; set; } = ChatBoxAlign.Bottom;

        public ChatBoxSettings()
        {

        }

        public ChatBoxSettings(AssetJson json)
        {
            FromJson(json);
        }

        public void Apply(ChatBox objection)
        {
            //objection.Text = Text;
        }

        public void FromJson(AssetJson json)
        {
            throw new NotImplementedException();
        }
    }
}
