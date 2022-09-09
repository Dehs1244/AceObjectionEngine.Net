using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Abstractions;
using AceObjectionEngine.Engine.Enums;
using AceObjectionEngine.Engine.Animator;

namespace AceObjectionEngine.Extensions
{
    public static class SpriteExtensions
    {
        public static ISpriteSource[] WithDelay(this ISpriteSource[] frames, TimeSpan delay, DelayMode mode = DelayMode.Freeze)
        {
            if (delay.Ticks < 1) return frames;
            var framesDuration = Frame.CalculateDuration(frames.Count());
            if (framesDuration >= delay) return frames;

            var allFrames = frames.ToList();
            var iteration = 0;

            while (Frame.CalculateDuration(allFrames.Count()) < framesDuration.Add(delay))
            {
                if (mode == DelayMode.Freeze) iteration = allFrames.Count - 1;
                allFrames.Add((ISpriteSource)allFrames[iteration].Clone());
                if (iteration >= allFrames.Count()) iteration = 0;
                if (mode != DelayMode.Freeze) iteration++;
            }

            var duration = Frame.CalculateDuration(allFrames.Count());

            return allFrames.ToArray();
        }

        [Obsolete("This method not work", true)]
        public static ISpriteSource[] WithSampling(this ISpriteSource[] frames, TimeSpan duration)
        {
            IList<ISpriteSource> allFrames = new List<ISpriteSource>();
            var framesDimension = (double)Frame.FrameCountFromDuration(duration);
            double samplingDiscriminator = Math.Ceiling((framesDimension - frames.Count()) / frames.Count());

            int iteration = 0;
            int samplingIteration = 0;

            while (Frame.CalculateDuration(allFrames.Count()) < duration)
            {
                allFrames.Add((ISpriteSource)frames[iteration].Clone());

                samplingIteration++;
                if (samplingIteration >= samplingDiscriminator)
                {
                    iteration++;
                    samplingIteration = 0;
                }
            }

            return allFrames.ToArray();
        }

        public static ISpriteSource[] WithDelay(this ICollection<ISpriteSource> frames, TimeSpan delay, DelayMode mode = DelayMode.Freeze) =>
            WithDelay(frames.ToArray(), delay, mode);

        public static ISpriteSource[] WithDelay(this IEnumerable<ISpriteSource> frames, TimeSpan delay, DelayMode mode = DelayMode.Freeze) =>
            WithDelay(frames.ToArray(), delay, mode);
    }
}
