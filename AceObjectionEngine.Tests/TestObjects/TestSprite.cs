using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Abstractions;

namespace AceObjectionEngine.Tests.TestObjects
{
    public sealed class TestSprite : ISpriteSource
    {
        public TimeSpan Delay { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool IsAnimated => throw new NotImplementedException();

        public bool IsFreezeLastOnDelay { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int Height => throw new NotImplementedException();

        public int Width => throw new NotImplementedException();

        public Bitmap RawBitmap => throw new NotImplementedException();

        public string Format => throw new NotImplementedException();

        public TimeSpan Duration => throw new NotImplementedException();

        public ISpriteSource[] AnimateFrames()
        {
            throw new NotImplementedException();
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public ISpriteSource MergeSprite(ISpriteSource other)
        {
            throw new NotImplementedException();
        }
    }
}
