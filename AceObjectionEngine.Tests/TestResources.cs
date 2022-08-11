using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Tests
{
    public static class TestResources
    {
        public static string ResourcePath => "Resources";
        public static string Mp3Audio1 => Path.Combine(ResourcePath, "audio1.mp3");
        public static string Background => Path.Combine(ResourcePath, "TestBackground.png");
        public static string AnimatedBackground => Path.Combine(ResourcePath, "TestAnimatedBackground.gif");
    }
}
