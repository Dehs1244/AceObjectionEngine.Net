﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Abstractions
{
    public interface ISpriteSource : IDisposable, ICloneable, IAnimationMedia
    {
        TimeSpan Delay { get; set; }
        bool IsAnimated { get; }
        bool IsFreezeLastOnDelay { get; set; }
        int Height { get; }
        int Width { get; }

        Bitmap RawBitmap { get; }
        ISpriteSource[] AnimateFrames();
        ISpriteSource MergeSprite(ISpriteSource other);
    }
}