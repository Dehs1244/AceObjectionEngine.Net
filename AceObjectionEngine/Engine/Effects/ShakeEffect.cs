using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Abstractions;
using AceObjectionEngine.Engine.Infrastructure;
using AceObjectionEngine.Engine.Model;

namespace AceObjectionEngine.Engine.Effects
{
    public class ShakeEffect : IAnimationEffect
    {
        public AcePoint ShakeRange { get; }
        public IAudioSource AudioSource { get; }
        public AceSize AnimatorSize { get; }

        public ShakeEffect(AceSize animatorSize, AcePoint range, IAudioSource audioSource)
        {
            ShakeRange = range;
            AnimatorSize = animatorSize;
            AudioSource = audioSource;
        }

        public ISpriteSource Implement(ISpriteSource sprite)
        {
            var gifSprite = new GifSprite();
            var currentShakeAmount = ShakeRange;
            while (gifSprite.Duration < AudioSource.Duration)
            {
                Bitmap frame = new Bitmap(AnimatorSize.Width, AnimatorSize.Height);

                using (Graphics graphics = Graphics.FromImage(frame))
                {
                    graphics.DrawImage(sprite.RawBitmap, currentShakeAmount.X, currentShakeAmount.Y);
                }

                if (ShakeRange.X != 0)
                {
                    if (currentShakeAmount.X > ShakeRange.X || currentShakeAmount.X < ShakeRange.X) currentShakeAmount = currentShakeAmount.Add(4f, 0);
                    else currentShakeAmount = currentShakeAmount.Add(-4f, 0);
                }

                if (ShakeRange.Y != 0)
                {
                    if (currentShakeAmount.Y > ShakeRange.Y || currentShakeAmount.Y < ShakeRange.Y) currentShakeAmount = currentShakeAmount.Add(0, 4f);
                    else currentShakeAmount = currentShakeAmount.Add(0, -4f);
                }

                gifSprite.AddFrame(new Sprite(frame));
            }

            return gifSprite;
        }
    }
}
