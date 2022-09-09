using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Abstractions;
using AceObjectionEngine.Engine.Animator;
using AceObjectionEngine.Engine.Attributes;
using AceObjectionEngine.Settings;
using AceObjectionEngine.Loader.Utils;
using AceObjectionEngine.Engine.Infrastructure;
using AceObjectionEngine.Abstractions.Layout;
using AceObjectionEngine.Helpers;

namespace AceObjectionEngine.Engine.Model.Components
{
    [ParallelAnimation]
    [ParallelAnimationOptions(AnimationStateBreaker.Origin, IsSkip = true)]
    public class Desk : IDesk
    {
        public int Id { get; }
        public string Name { get; }

        public ISpriteSource Sprite { get; }

        public IAudioSource AudioSource => null;

        public TimeSpan DurationCounter => TimeSpan.Zero;

        public SingleEntry<int> LayerIndexer { get; set; }

        public Type[] DependencyIndexer => EnumerableHelper.ToEnumerable(typeof(Background)).ToArray();

        public Desk(IObjectionSettings<Desk> settings)
        {
            settings.Apply(this);
        }

        public Desk(DeskSettings settings)
        {
            Sprite = MediaMaker.SpriteMaker.Make(settings.ImagePath);
        }

        public void Dispose()
        {
            Sprite?.Dispose();
            //AudioSource?.Dispose();
        }

        public void EndAnimation()
        {
        }

        public Task EndAnimationAsync() => Task.Run(() => EndAnimation());

        public void StartAnimation()
        {
        }

        public Task StartAnimationAsync() => Task.Run(() => StartAnimation());
    }
}
