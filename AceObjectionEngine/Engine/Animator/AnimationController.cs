using AceObjectionEngine.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Engine.Animator
{
    internal static class AnimationController
    {
        public static IList<ISpriteSource> MergeAnimations(IList<ISpriteSource> firstFrames, IList<ISpriteSource> secondFrames, Attributes.AnimationStateBreaker breaker, bool repeatOnBreak = false)
        {
            if (!firstFrames.Any()) return secondFrames;
            if (!secondFrames.Any()) return firstFrames;

            IList<ISpriteSource> renderedParallelAnimation = new List<ISpriteSource>();
            var firstFrameIteration = 0;
            var secondFrameIteration = 0;

            var processingCounter = breaker == Attributes.AnimationStateBreaker.Parallel ? firstFrames.Count : secondFrames.Count;

            for (var i = 0; i < processingCounter; i++)
            {
                var passiveAnimationFrameError = breaker == Attributes.AnimationStateBreaker.Parallel ?
                secondFrameIteration >= secondFrames.Count
                : firstFrameIteration >= firstFrames.Count;

                if (passiveAnimationFrameError)
                {
                    //if (context.Breaker == Attributes.AnimationStateBreaker.Origin) break;
                    switch (breaker)
                    {
                        case Attributes.AnimationStateBreaker.Origin:
                            if (!repeatOnBreak) secondFrames.Add((ISpriteSource)firstFrames.Last().Clone());
                            else firstFrameIteration = 0;
                            break;
                        case Attributes.AnimationStateBreaker.Parallel:
                            if (!repeatOnBreak) secondFrames.Add((ISpriteSource)secondFrames.Last().Clone());
                            else secondFrameIteration = 0;
                            break;
                    }
                }
                var frame = (ISpriteSource)firstFrames[firstFrameIteration].Clone();

                renderedParallelAnimation.Add(frame.MergeSprite(secondFrames[secondFrameIteration]));

                secondFrameIteration++;
                firstFrameIteration++;
            }

            return renderedParallelAnimation;
        }
    }
}
