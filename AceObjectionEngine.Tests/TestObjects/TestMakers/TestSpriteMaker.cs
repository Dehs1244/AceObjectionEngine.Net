using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Abstractions;

namespace AceObjectionEngine.Tests.TestObjects.TestMakers
{
    public sealed class TestSpriteMaker : IMediaMaker<ISpriteSource>
    {
        public ISpriteSource Make(string filePath) => new TestSprite();

        public ISpriteSource Make(string filePath, params object[] args) => new TestSprite();
    }
}
