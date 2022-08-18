using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Abstractions;
using AceObjectionEngine.Loader.Presets;
using AceObjectionEngine.Engine.Model.Settings;
using AceObjectionEngine.Loader.Utils;
using AceObjectionEngine.Engine.Attributes;
using AceObjectionEngine.Engine.Infrastructure;

namespace AceObjectionEngine.Engine.Model.Layout
{
    [ParallelAnimation]
    [ParallelAnimationOptions(IsSkip = false)]
    public class Background : IObjectionObject
    {
        public int Id { get; }
        public string Name { get; }
        public bool IsWide { get; set; }
        public TimeSpan DurationCounter => TimeSpan.Zero;

        public ISpriteSource Sprite { get; }
        public Desk Desk { get; }
        public string ImagePath { get; }
        public string ImageDeskPath { get; }

        public IAudioSource AudioSource => null;

        public Background(IObjectionSettings<Background> settings)
        {
            Id = settings.Id;
            settings.Apply(this);
        }

        public Background(BackgroundSettings settings)
        {
            Id = settings.Id;
            settings.Apply(this);
            Sprite =  MediaMaker.SpriteMaker.Make(settings.ImagePath);
            Name = settings.Name;
            ImagePath = settings.ImagePath;
            ImageDeskPath = settings.ImageDeskPath;
            if (settings.ImageDeskPath != null) Desk = new Desk(new DeskSettings()
            {
                ImagePath = settings.ImageDeskPath
            });

            IsWide = settings.IsWide;
        }

        public Background WithSettings(BackgroundSettings settings)
        {
            settings.ImagePath = settings.ImagePath ?? ImagePath;
            settings.ImageDeskPath = settings.ImageDeskPath ?? ImageDeskPath;
            settings.Name = settings.Name ?? Name;

            return new Background(settings);
        }

        public void Dispose()
        {
            Sprite?.Dispose();
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
