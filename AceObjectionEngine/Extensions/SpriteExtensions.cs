using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Abstractions;
using AceObjectionEngine.Engine.Animator;

namespace AceObjectionEngine.Extensions
{
    public static class SpriteExtensions
    {
        public static ISpriteSource[] WithDelay(this ISpriteSource[] frames, TimeSpan delay, bool isFreezeOnLast = false)
        {
            if (delay.Ticks < 1) return frames;

            var allFrames = frames.ToList();
            var iteration = 0;
            while (Frame.CalculateDuration(allFrames.Count()) < Frame.CalculateDuration(frames.Count()).Add(delay))
            {
                if (isFreezeOnLast) iteration = allFrames.Count - 1;
                allFrames.Add((ISpriteSource)allFrames[iteration].Clone());
                if (iteration >= allFrames.Count()) iteration = 0;
                if(!isFreezeOnLast) iteration++;
            }

            return allFrames.ToArray();
        }

        public static ISpriteSource[] WithDelay(this ICollection<ISpriteSource> frames, TimeSpan delay, bool isFreezeOnLast = false) =>
            WithDelay(frames.ToArray(), delay, isFreezeOnLast);

        public static ISpriteSource[] WithDelay(this IEnumerable<ISpriteSource> frames, TimeSpan delay, bool isFreezeOnLast = false) =>
            WithDelay(frames.ToArray(), delay, isFreezeOnLast);
    }
}
