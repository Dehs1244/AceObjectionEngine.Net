using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Abstractions;
using AceObjectionEngine.Abstractions.Layout;
using AceObjectionEngine.Engine.Animator;
using AceObjectionEngine.Helpers;
using AceObjectionEngine.Engine.Model;

namespace AceObjectionEngine.Engine.PoseActions
{
    public class IdleWithChatBoxPoseAction : IRenderBranch
    {
        public ISpriteSource State { get; set; }
        public TimeSpan Delay { get; set; }
        public TimeSpan Duration => TimeSpan.FromSeconds(3);

        public RenderActionConsequence Action(AnimationRenderContext sourceContext, ICollection<IAnimationObject> parallelObjects)
        {
            var chatBoxes = parallelObjects.Where(x => x is IChatBox);
            var desks = parallelObjects.Where(x => x is IDesk);
            if (chatBoxes.Any())
            {
                var chatBox = chatBoxes.Cast<IChatBox>().First();
                var sourceAnimated = sourceContext.Sprite.AnimateFrames();
                if (desks.Any()) sourceAnimated = AnimationController.MergeAnimations(sourceAnimated, desks.First().Sprite.AnimateFrames(), sourceContext.ParallelOptions.InvertBreaker(), sourceContext.RepeatOnBreak).ToArray();

                var animatedFrame = chatBox.Sprite.AnimateFrames().Last();
                animatedFrame.Delay = TimeSpan.FromSeconds(3);

                var consequence = desks.Any() ?
                    new RenderActionConsequence(AnimationController.MergeAnimations(sourceAnimated, animatedFrame.AnimateFrames(), sourceContext.SourceBreaker, sourceContext.RepeatOnBreak), desks.First()) :
                    new RenderActionConsequence(AnimationController.MergeAnimations(sourceAnimated, animatedFrame.AnimateFrames(), sourceContext.SourceBreaker, sourceContext.RepeatOnBreak));
                return consequence;
            }

            return RenderActionConsequence.FromEmpty();
        }
    }
}
