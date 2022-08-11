using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Engine.Attributes;
using AceObjectionEngine.Abstractions;
using AceObjectionEngine.Helpers;
using AceObjectionEngine.Abstractions.Layout.Character;
using AceObjectionEngine.Engine.Model;

namespace AceObjectionEngine.Engine.Animator
{
    public struct AnimationRenderContext
    {
        public bool IsParallel => ParallelAttribute != null;
        public bool IsMinorObject { get; }
        public bool IsRerenderable => AnimationObject is IRerenderable;
        public ParallelAnimationAttribute ParallelAttribute;
        public ParallelAnimationOptionsAttribute ParallelOptions;

        public IAnimationObject AnimationObject;
        public ISpriteSource Sprite => AnimationObject.Sprite;
        public IAudioSource AudioSource => AnimationObject.AudioSource;
        public IAudioSource[] AudioTicks => AnimationObject is ICharacter character ? 
            character.ActivePose.AudioTicks 
            : Array.Empty<IAudioSource>();

        public AnimationStateBreaker Breaker => ParallelOptions.Breaker;
        public bool RepeatOnBreak => ParallelOptions.RepeatOnBreak;
        public bool IsSkipRender => ParallelOptions.IsSkip;

        public TimeSpan CurrentRenderFrameDuration;
        public TimeSpan CurrentAnimationDuration;
        public TimeSpan EndAnimationDuration;
        public TimeSpan EndRenderFrameDuration;

        public AnimationRenderContext(IAnimationObject animationObject)
        {
            AnimationObject = animationObject;

            ParallelAttribute = TypeHelper.GetExtensiveAttribute<ParallelAnimationAttribute>(AnimationObject);

            ParallelOptions = ParallelAttribute != null
                ? TypeHelper.GetExtensiveAttribute<ParallelAnimationOptionsAttribute>(AnimationObject) ?? ParallelAnimationOptionsAttribute.Default : null;
            CurrentRenderFrameDuration = new TimeSpan();
            EndRenderFrameDuration = new TimeSpan();
            CurrentAnimationDuration = new TimeSpan();
            EndAnimationDuration = new TimeSpan();
            IsMinorObject = TypeHelper.GetExtensiveAttribute<MinorObjectAttribute>(AnimationObject) != null;
        }

        public bool CanParallelRender()
        {
            if (!IsParallel) return false;

            return ParallelAttribute.Inspect(AnimationObject);
        }

        public RenderActionConsequence InvokeOnRender(AnimationRenderContext context, ICollection<IAnimationObject> parallelObjects)
        {
            if(AnimationObject is IRenderInvokable invokable)
            {
                return invokable.OnRenderInvoke(context, parallelObjects);
            }

            return RenderActionConsequence.FromEmpty();
        }

        public TimeSpan GetDuration() => AnimationObject.Sprite != null ? AnimationObject.Sprite.Duration : AnimationObject.AudioSource.Duration;

        public bool IsNeedReRender() => AnimationObject is IRerenderable reRenderable ? reRenderable.IsNeedReRender : false;
    }
}
