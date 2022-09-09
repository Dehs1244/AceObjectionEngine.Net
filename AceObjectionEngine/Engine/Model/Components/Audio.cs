using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Abstractions;
using AceObjectionEngine.Loader.Presets;
using AceObjectionEngine.Settings;
using System.IO;
using AceObjectionEngine.Loader.Utils;
using Newtonsoft.Json.Linq;
using AceObjectionEngine.Engine.Infrastructure;

namespace AceObjectionEngine.Engine.Model.Components
{
    public sealed class Audio : IObjectionObject
    {
        public int Id { get; }
        public string Name { get; }
        public string Path;
        public long Size { get; }
        public TimeSpan DurationCounter => TimeSpan.Zero;

        public ISpriteSource Sprite => null;

        public IAudioSource AudioSource { get; }

        public float Volume;

        public Audio(IObjectionSettings<Audio> settings)
        {
            Id = settings.Id;
            settings.Apply(this);
        }

        public Audio(SoundSettings settings)
        {
            Id = settings.Id;
            settings.Apply(this);
            Path = settings.AudioPath;
            Name = settings.Name;
            Volume = settings.Volume;
            AudioSource = MediaMaker.AudioSourceMaker.Make(settings.AudioPath, settings.StartPlay);
            AudioSource.IsFixate = settings.IsFixate ?? false;
            Size = new FileInfo(Path).Length;
        }

        public Audio WithSettings(SoundSettings settings)
        {
            settings.Name = settings.Name ?? Name;
            settings.IsFixate = settings.IsFixate ?? false;

            if (settings.Volume == 0f) settings.Volume = Volume;

            return new Audio(settings);
        }

        public void Dispose()
        {
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
