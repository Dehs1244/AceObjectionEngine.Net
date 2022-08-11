using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Loader.Assets;
using Xunit;

namespace AceObjectionEngine.Tests.Loader
{
    public class AssetTest
    {
        [Theory]
        [InlineData(1)]
        public void AssetBackgroundTest(int id)
        {
            BackgroundLoader loader = new BackgroundLoader(id);
            var background = loader.Load();
            Assert.NotNull(background);
        }

        [Fact]
        public void NonIdAssetBackgroundTest()
        {
            BackgroundLoader loader = new BackgroundLoader();
            var backgrounds = loader.LoadAll();
            Assert.Empty(backgrounds);
        }

        [Theory]
        [InlineData(1)]
        public void AssetCharacterTest(int id)
        {
            CharacterLoader loader = new CharacterLoader(id);
            var character = loader.Load();
            Assert.NotNull(character);
        }

        [Theory]
        [InlineData(1)]
        public void AssetSoundTest(int id)
        {
            SoundLoader loader = new SoundLoader(id);
            var sound = loader.Load();
            Assert.NotNull(sound);
        }

        [Theory]
        [InlineData(0)]
        public void AssetBlibMaleSoundTest(int id)
        {
            BlipAudioAsset blipLoader = new BlipAudioAsset(id);
            var sound = blipLoader.Load();
            Assert.NotNull(sound);
        }

        [Theory]
        [InlineData(1)]
        public void AssetBlibFemaleSoundTest(int id)
        {
            BlipAudioAsset blipLoader = new BlipAudioAsset(id);
            var sound = blipLoader.Load();
            Assert.NotNull(sound);
        }

        [Theory]
        [InlineData(1)]
        public void AssetMusicTest(int id)
        {
            MusicLoader loader = new MusicLoader(id);
            var music = loader.Load();
            Assert.NotNull(music);
        }
    }
}
