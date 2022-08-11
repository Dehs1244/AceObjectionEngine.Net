using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Abstractions;
using AceObjectionEngine.Engine.Animator;

namespace AceObjectionEngine
{
    public class ObjectionSettings
    {
        public int AudioLayer { get; set; } = 0;
        public int BackgroundLayer { get; set; } = 1;
        public int CharacterLayer { get; set; } = 2;
        public int DialogueLayer { get; set; } = 3;
        public int BubbleLayer { get; set; } = 10;

        public int Width { get; set; } = 960;
        public int Height { get; set; } = 640;
        public IFrameRenderer<Frame> FrameRenderer { get; set; }

        public static ObjectionSettings Default => new ObjectionSettings()
        {
            AudioLayer = 0,
            BackgroundLayer = 1,
            CharacterLayer = 2,
            DialogueLayer = 3,
            BubbleLayer = 10,
            FrameRenderer = null,
            Width = 960,
            Height = 640
        };
    }
}
