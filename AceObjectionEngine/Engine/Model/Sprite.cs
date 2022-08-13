using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AceObjectionEngine.Abstractions;
using AceObjectionEngine.Abstractions.Async;
using AceObjectionEngine.Exceptions;
using FFMpegCore.Pipes;
using AceObjectionEngine.Engine.Animator;
using AceObjectionEngine.Extensions;
using AceObjectionEngine.Engine.Enums;

namespace AceObjectionEngine.Engine.Model
{
    public delegate void SpriteAnimating(int frame, ISpriteSource frameSprite);
    public sealed class Sprite : IAnimationObject, IVideoFrame, ISpriteSource
    {
        public int OffsetX { get; private set; }
        public int OffsetY { get; private set; }
        public int Height { get; private set; }
        public int Width { get; private set; }
        public bool IsAnimated { get; }

        public TimeSpan Duration { get; }
        public TimeSpan DurationCounter => Duration;

        public int Fps { get; }
        public int FrameCount { get; }
        private int _frameAnimating;
        public IAudioSource AudioSource { get; }
        ISpriteSource IAnimationObjectAsync.Sprite => this;
        public TimeSpan Delay { get; set; }

        public event SpriteAnimating OnAnimation;
        public Bitmap RawBitmap { get; private set; }

        public string Format
        {
            get
            {
                switch (RawBitmap.PixelFormat)
                {
                    case PixelFormat.Format16bppGrayScale:
                        return "gray16le";
                    case PixelFormat.Format16bppRgb555:
                        return "bgr555le";
                    case PixelFormat.Format16bppRgb565:
                        return "bgr565le";
                    case PixelFormat.Format24bppRgb:
                        return "bgr24";
                    case PixelFormat.Format32bppArgb:
                        return "bgra";
                    case PixelFormat.Format32bppPArgb:
                        return "argb";
                    case PixelFormat.Format32bppRgb:
                        return "rgba";
                    case PixelFormat.Format48bppRgb:
                        return "rgb48le";
                    default:
                        throw new NotSupportedException($"Not supported pixel format {RawBitmap.PixelFormat}");
                };
            }
        }

        public DelayMode DelayMode { get; set; }

        public Sprite(Bitmap source)
        {
            Height = source.Height;
            RawBitmap = source;
            Fps = ObjectionAnimator.FrameRate;
            try
            {
                if (TryGetRawDuration(out TimeSpan? duration))
                {
                    Duration = duration.Value;
                    IsAnimated = true;
                    FrameCount = GetFrameCount();
                }
                else Duration = Frame.CalculateDuration(1);
                Width = source.Width;
            }
            catch
            {
                throw new ObjectionVisorObjectException<Sprite>();
            }
        }

        public Sprite(Image source) : this(new Bitmap(source))
        {
        }

        public Sprite(Bitmap source, IAudioSource audio) : this(source)
        {
            AudioSource = audio;
        }

        public Sprite(Image source, IAudioSource audio) : this(source)
        {
            AudioSource = audio;
        }

        public int GetFrameCount()
        {
            if (!ImageAnimator.CanAnimate(RawBitmap)) return 0;

            var frameDimension = new FrameDimension(RawBitmap.FrameDimensionsList[0]);

            return RawBitmap.GetFrameCount(frameDimension);
        }

        public bool TryGetRawDuration(out TimeSpan? duration)
        {
            var minimumFrameDelay = (1000.0 / ObjectionAnimator.FrameRate);
            duration = null;
            //if (!RawBitmap.RawFormat.Equals(ImageFormat.Gif)) return false;
            if (!ImageAnimator.CanAnimate(RawBitmap)) return false;

            var frameCount = GetFrameCount();
            duration = Frame.CalculateDuration(frameCount);
            //var totalDuration = 0;
            //
            //for (var f = 0; f < frameCount; f++)
            //{
            //    var delayPropertyBytes = RawBitmap.GetPropertyItem(20736).Value;
            //    var frameDelay = BitConverter.ToInt32(delayPropertyBytes, f * 4) * 10;
            //    totalDuration += (frameDelay < minimumFrameDelay ? (int)minimumFrameDelay : frameDelay);
            //}
            //duration = TimeSpan.FromMilliseconds(totalDuration);

            return true;
        }

        public ISpriteSource MergeSprite(ISpriteSource other)
        {
            using (Graphics g = Graphics.FromImage(RawBitmap))
            {
                g.DrawImage(other.RawBitmap, Point.Empty);
            }

            return this;
        }

        public void Serialize(Stream stream)
        {
            var data = RawBitmap.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadOnly, RawBitmap.PixelFormat);

            try
            {
                var buffer = new byte[data.Stride * data.Height];
                Marshal.Copy(data.Scan0, buffer, 0, buffer.Length);
                stream.Write(buffer, 0, buffer.Length);
            }
            finally
            {
                RawBitmap.UnlockBits(data);
            }
        }

        public async Task SerializeAsync(Stream stream, CancellationToken token)
        {
            var data = RawBitmap.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadOnly, RawBitmap.PixelFormat);

            try
            {
                var buffer = new byte[data.Stride * data.Height];
                Marshal.Copy(data.Scan0, buffer, 0, buffer.Length);
                await stream.WriteAsync(buffer, 0, buffer.Length, token).ConfigureAwait(false);
            }
            finally
            {
                RawBitmap.UnlockBits(data);
            }
        }

        public ISpriteSource[] AnimateFrames()
        {
            if (!IsAnimated) return new Sprite[1] { this }.WithDelay(Delay, DelayMode);

            ICollection<ISpriteSource> frames = new List<ISpriteSource>();
            FrameDimension dimension = new FrameDimension(RawBitmap.FrameDimensionsList[0]);
            do
            {
                RawBitmap.SelectActiveFrame(dimension, _frameAnimating);
                frames.Add((Sprite)Clone());
                OnAnimation?.Invoke(_frameAnimating, frames.Last());
                _frameAnimating++;
            } while (_frameAnimating < FrameCount);

            _frameAnimating = 0;

            return frames.WithDelay(Delay, DelayMode);
        }

        public object Clone() => new Sprite(new Bitmap(RawBitmap));

        public static Sprite FromFile(string file)
        {
            try
            {
                return new Sprite(new Bitmap(file, true));
            }
            catch
            {
                throw new ObjectionVisorObjectException<Sprite>(file);
            }
        }

        public void Dispose()
        {
            RawBitmap.Dispose();
        }

        public void EndAnimation()
        {
        }

        public async Task EndAnimationAsync() => await Task.Run(EndAnimation);

        public void StartAnimation()
        {
        }

        public async Task StartAnimationAsync() => await Task.Run(StartAnimation);
    }
}
