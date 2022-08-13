using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Abstractions;
using AceObjectionEngine.Engine.Model.Settings;
using AceObjectionEngine.Loader.Presets;
using AceObjectionEngine.Loader.Utils;
using AceObjectionEngine.Engine.Attributes;
using AceObjectionEngine.Engine.Infrastructure;
using System.Drawing;

namespace AceObjectionEngine.Engine.Model.Layout
{
    [MinorObject]
    public class Bubble : IObjectionObject
    {
        public int Id { get; }
        public string Name { get; }
        public string Path { get; }
        public TimeSpan DurationCounter => Sprite.Duration;

        public ISpriteSource Sprite { get; private set; }

        private ISpriteSource _sourceBublleSprite;
        public IAudioSource AudioSource { get; }

        public Bubble(IObjectionSettings<Bubble> settings)
        {
            Id = settings.Id;
            settings.Apply(this);
        }

        public Bubble(BubbleSettings settings)
        {
            Id = settings.Id;
            settings.Apply(this);
            Path = settings.ImagePath;
            Name = settings.Name;
            _sourceBublleSprite = MediaMaker.SpriteMaker.Make(settings.ImagePath);
            AudioSource = settings.Audio;

            Sprite = _DrawSprite(settings.Effects);
        }

        private ISpriteSource _DrawSprite(IEnumerable<IAnimationEffect> effects)
        {
            ISpriteSource sprite = null;
            foreach (var effect in effects)
            {
                sprite = effect.Implement(_sourceBublleSprite);
            }
            return sprite;
        }

        public void Dispose()
        {
            Sprite.Dispose();
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
