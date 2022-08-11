using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Abstractions;
using AceObjectionEngine.Engine.Animator;
using AceObjectionEngine.Extensions;

namespace AceObjectionEngine.Engine.Model
{
    public class GifSprite : ISpriteSource, IEnumerable<ISpriteSource>, IEnumerator<ISpriteSource>
    {
        public string Format => "GIF";

        public ISpriteSource this[int index] => index < 0 || index >= _frames.Count ? null : _frames.ElementAt(index);

        public TimeSpan Delay { get; set; }
        public TimeSpan Duration => Frame.CalculateDuration(FrameCount) + Delay;
        public int FrameCount => _frames.Count;
        private int _framePosition = -1;

        private readonly ICollection<ISpriteSource> _frames = new List<ISpriteSource>();

        public bool IsAnimated => true;

        public int Height { get; set; }

        public int Width { get; set; }

        public Bitmap RawBitmap => _frames.Any() ? _frames.First().RawBitmap : null;

        public ISpriteSource Current
        {
            get
            {
                if (_framePosition < 0 || _framePosition >= _frames.Count)
                    return null;
                return _frames.ElementAt(_framePosition);
            }
        }

        object IEnumerator.Current => Current;

        public bool IsFreezeLastOnDelay { get; set; }

        public ISpriteSource[] AnimateFrames() => _frames.WithDelay(Delay, IsFreezeLastOnDelay);

        public object Clone() => (GifSprite)MemberwiseClone();

        public void Dispose()
        {
            foreach(var sprite in _frames)
            {
                sprite.Dispose();
            }
        }

        public ISpriteSource MergeSprite(ISpriteSource other)
        {
            foreach(var frameSprite in _frames)
            {
                frameSprite.MergeSprite(other);
            }

            return this;
        }

        public IEnumerator<ISpriteSource> GetEnumerator() => _frames.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _frames.GetEnumerator();

        public bool MoveNext()
        {
            if (_framePosition < _frames.Count)
            {
                _framePosition++;
                return true;
            }
            else
                return false;
        }

        public void Reset() => _framePosition = -1;

        public void AddFrame(ISpriteSource sprite) => _frames.Add(sprite);
    }
}
