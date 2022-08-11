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
    public class IdleWithChatBoxPoseAction : IPoseAction
    {
        public ISpriteSource State { get; set; }
        public TimeSpan Delay { get; set; }
        public TimeSpan Duration => TimeSpan.FromSeconds(3);

        public RenderActionConsequence Action(AnimationRenderContext sourceContext, ICollection<IAnimationObject> parallelObjects)
        {
            var chatBoxes = parallelObjects.Where(x => x is IChatBox);
            if (chatBoxes.Any())
            {
                var chatBox = chatBoxes.Cast<IChatBox>().First();
                var animatedFrame = chatBox.Sprite.AnimateFrames().Last();
                animatedFrame.Delay = TimeSpan.FromSeconds(3);
                return new RenderActionConsequence(FrameRenderFactory.MergeAnimations(sourceContext.Sprite.AnimateFrames(), animatedFrame.AnimateFrames(), sourceContext));
            }

            return RenderActionConsequence.FromEmpty();
        }
    }
}
