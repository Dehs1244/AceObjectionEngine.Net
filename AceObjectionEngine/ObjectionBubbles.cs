// Auto-generated code
using System;
using AceObjectionEngine;
using AceObjectionEngine.Engine.Model.Components;
using AceObjectionEngine.Loader.Presets;

namespace AceObjectionEngine
{
    public static partial class ObjectionBubbles
    {

        public static Bubble Objection => new BubblePresetLoader(1).LoadObject();

        public static Bubble HoldIt => new BubblePresetLoader(2).LoadObject();

        public static Bubble TakeThat => new BubblePresetLoader(3).LoadObject();

    }
}