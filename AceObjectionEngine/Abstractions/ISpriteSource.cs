using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Engine.Enums;

namespace AceObjectionEngine.Abstractions
{
    /// <summary>
    /// Interface for implementing image processing and animating
    /// </summary>
    public interface ISpriteSource : IDisposable, ICloneable, IAnimationMedia
    {
        /// <summary>
        /// Delay after animation of this image
        /// </summary>
        TimeSpan Delay { get; set; }
        /// <summary>
        /// Delay method
        /// </summary>
        DelayMode DelayMode { get; set; }
        /// <summary>
        /// Checks whether the given image is animated
        /// </summary>
        bool IsAnimated { get; }
        /// <summary>
        /// Height of Image
        /// </summary>
        int Height { get; }
        /// <summary>
        /// Width of Image
        /// </summary>
        int Width { get; }

        /// <summary>
        /// Gets the processed image directly
        /// </summary>
        Bitmap RawBitmap { get; }
        /// <summary>
        /// Animates the sprite
        /// </summary>
        /// <returns>Collection of animated frames</returns>
        ISpriteSource[] AnimateFrames();
        /// <summary>
        /// Merge a particular <see cref="ISpriteSource"/> with another <see cref="ISpriteSource"/> and dispose it
        /// </summary>
        /// <param name="other">The <see cref="ISpriteSource"/> to be overlaid</param>
        /// <returns>Merged <see cref="ISpriteSource"/></returns>
        ISpriteSource MergeSprite(ISpriteSource other);
    }
}
