using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using AceObjectionEngine.Loader.Presets;
using AceObjectionEngine.Engine.Model.Components;

namespace AceObjectionEngine.Tests.Loader
{
    public class PresetTest
    {
        [Theory]
        [InlineData(1)]
        public void LoadBackgroundTest(int id)
        {
            BackgroundPreset preset = new BackgroundPreset(id);
            var background = preset.Load();
            Assert.IsType<Background>(background);
        }

        [Theory]
        [InlineData(1)]
        public async Task LoadBackgroundAsyncTest(int id)
        {
            BackgroundPreset preset = new BackgroundPreset(id);
            var background = await preset.LoadAsync();
            Assert.IsType<Background>(background);
        }

        [Theory]
        [InlineData(1)]
        public void LoadCharacterTest(int id)
        {
            CharacterPreset preset = new CharacterPreset(id);
            var character = preset.Load();
            Assert.IsType<Character>(character);
        }

        [Theory]
        [InlineData(1)]
        public async Task LoadCharacterAsyncTest(int id)
        {
            CharacterPreset preset = new CharacterPreset(id);
            var character = await preset.LoadAsync();
            Assert.IsType<Character>(character);
        }

        [Theory]
        [InlineData(1)]
        public void LoadAudioTest(int id)
        {
            BlipAudioPreset preset = new BlipAudioPreset(AceObjectionEngine.Engine.Enums.BlipSexType.Male);
            var audio = preset.Load();
            Assert.IsType<Audio>(audio);
        }

        [Theory]
        [InlineData(1)]
        public async Task LoadAudioAsyncTest(int id)
        {
            BlipAudioPreset preset = new BlipAudioPreset(AceObjectionEngine.Engine.Enums.BlipSexType.Male);
            var audio = await preset.LoadAsync();
            Assert.IsType<Audio>(audio);
        }

        [Theory]
        [InlineData(1)]
        public void LoadSoundTest(int id)
        {
            SoundPreset preset = new SoundPreset(id);
            var audio = preset.Load();
            Assert.IsType<Audio>(audio);
        }

        [Theory]
        [InlineData(1)]
        public async Task LoadSoundAsyncTest(int id)
        {
            SoundPreset preset = new SoundPreset(id);
            var audio = await preset.LoadAsync();
            Assert.IsType<Audio>(audio);
        }
    }
}
