using AceObjectionEngine.Abstractions;
using AceObjectionEngine.Engine.Model;
using AceObjectionEngine.Engine.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Drawing;

namespace AceObjectionEngine.Tests.Engine
{
    public class GifTest
    {
        [Fact]
        public void WriterTest()
        {
            List<ISpriteSource> sprites = new();
            //sprites.Add(Sprite.FromFile("Resources/1.png"));
            //sprites.Add(Sprite.FromFile("Resources/2.png"));
            var testBitmap = new Bitmap(960, 640);

        }
    }
}
