using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Abstractions;
using AceObjectionEngine.Engine.MediaMakers;

namespace AceObjectionEngine.Engine.Infrastructure
{
    /// <summary>
    /// Initialization class of Media
    /// </summary>
    public static class MediaMaker
    {
        private static Lazy<IMediaMaker<ISpriteSource>> _spriteMaker = new Lazy<IMediaMaker<ISpriteSource>>(() => new DefaultSpriteMaker());
        public static IMediaMaker<ISpriteSource> SpriteMaker => _spriteMaker.Value;

        private static Lazy<IMediaMaker<IAudioSource>> _audioSourceMaker = new Lazy<IMediaMaker<IAudioSource>>(() => new DefaultAudioSourceMaker());
        public static IMediaMaker<IAudioSource> AudioSourceMaker => _audioSourceMaker.Value;

        public static void SetSpriteMaker(Func<IMediaMaker<ISpriteSource>> maker)
        {
            _spriteMaker = new Lazy<IMediaMaker<ISpriteSource>>(maker);
        }

        public static void SetAudioSourceMaker(Func<IMediaMaker<IAudioSource>> maker)
        {
            _audioSourceMaker = new Lazy<IMediaMaker<IAudioSource>>(maker);
        }
    }
}
