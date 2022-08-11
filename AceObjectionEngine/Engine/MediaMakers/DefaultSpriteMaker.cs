using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Abstractions;
using AceObjectionEngine.Engine.Model;

namespace AceObjectionEngine.Engine.MediaMakers
{
    public class DefaultSpriteMaker : IMediaMaker<ISpriteSource>
    {
        public ISpriteSource Make(string filePath) => Sprite.FromFile(filePath);

        public ISpriteSource Make(string filePath, params object[] args) => Make(filePath);
    }
}
