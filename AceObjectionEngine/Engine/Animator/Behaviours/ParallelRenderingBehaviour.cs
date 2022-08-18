using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Abstractions;
using AceObjectionEngine.Engine.Animator;
using AceObjectionEngine.Helpers;

namespace AceObjectionEngine.Engine.Animator.Behaviours
{
    public class ParallelRenderingBehaviour : IAnimationBehaviour
    {
        public AnimationRenderContext[] Contexts { get; }
        private FrameRenderFactory _renderFactory { get; }
        public int RendersToSkip;

        private List<Type> _closedRenders = new List<Type>();
        private List<IAnimationObject> _animatedObjects = new List<IAnimationObject>();

        public ParallelRenderingBehaviour(FrameRenderFactory factory, AnimationRenderContext[] layers)
        {
            Contexts = layers;
            _renderFactory = factory;
        }

        private bool _IsNeedToAnimate(IAnimationObject animationObject) => !_closedRenders.Any(x=> TypeHelper.AreSame(x, animationObject.GetType()));

        public ISpriteSource[] Use(int index)
        {

            var isNeedToAnimate = _IsNeedToAnimate(Contexts[index].AnimationObject);
            if (Contexts[index].IsNonRenderObject) return Array.Empty<ISpriteSource>();

            if(isNeedToAnimate) _animatedObjects.Add(Contexts[index].AnimationObject);

            List<ISpriteSource> animationCurrentFrames = new List<ISpriteSource>();

            var renderInvoke = isNeedToAnimate ?
                Contexts[index].InvokeOnRender(Contexts[index], Contexts.Select(x => x.AnimationObject).ToList())
                : Model.RenderActionConsequence.FromEmpty();

            if (renderInvoke.IsAction)
            {
                animationCurrentFrames = renderInvoke.Animation.ToList();
                if (renderInvoke.ClosedForRender.Any()) _closedRenders.AddRange(renderInvoke.ClosedForRender);
            }
            else if (isNeedToAnimate) animationCurrentFrames = Contexts[index].Sprite.AnimateFrames().ToList();

            RendersToSkip = index;
            if (index + 1 < Contexts.Count() &&
                Contexts[index].IsParallel)
            {
                if (!Contexts[index].CanParallelRender() &&
                    Contexts[index].DeceptionLayers != null) _closedRenders.AddRange(Contexts[index].DeceptionLayers.Misleaders);
                var parallelFrames = Use(index + 1);
                List<ISpriteSource> renderedParallelAnimation = new List<ISpriteSource>();
                var breaker = _animatedObjects.Any(x => Contexts[index].ParallelOptions.CheckForBreakOnly(x)) ? Contexts[index].SourceBreaker 
                    : Contexts[index].ParallelOptions.InvertBreaker();

                renderedParallelAnimation.AddRange(FrameRenderFactory.MergeAnimations(animationCurrentFrames, parallelFrames, breaker, Contexts[index].RepeatOnBreak));

                animationCurrentFrames = renderedParallelAnimation;
            }

            if (Contexts[index].AudioTicks.Count() > 0)
                _renderFactory.RenderAudio(EnumerableHelper.ToEnumerable(Contexts[index]).ToArray());

            Contexts[index].AnimationObject.EndAnimation();

            if (Contexts[index].IsRerenderable) _renderFactory.AddTime(Frame.CalculateDuration(animationCurrentFrames.Count()));

            if (Contexts[index].IsNeedReRender())
            {
                _animatedObjects.Clear();
                _closedRenders.Clear();
                var rerendered = Use(0);
                animationCurrentFrames.AddRange(rerendered);
            }

            Contexts[index].CurrentAnimationDuration = _renderFactory.TimeLineAnimationDuration;
            Contexts[index].CurrentRenderFrameDuration = _renderFactory.TimeLineRenderDuration;
            Contexts[index].EndAnimationDuration = Contexts[index].AnimationObject.Sprite.Duration;
            if (Contexts[index].AudioSource != null && isNeedToAnimate)
                _renderFactory.RenderAudio(EnumerableHelper.ToEnumerable(Contexts[index]).ToArray());

            //animationCurrentFrames.AddRange(UseParallelAnimation(layerCollection, index + 1));
            return animationCurrentFrames.ToArray();
        }
    }
}
