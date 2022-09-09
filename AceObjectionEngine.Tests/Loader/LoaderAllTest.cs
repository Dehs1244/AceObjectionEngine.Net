using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AceObjectionEngine.Loader.Presets;
using AceObjectionEngine.Engine.Model.Components;
using System.Threading.Tasks;
using Xunit;

namespace AceObjectionEngine.Tests.Loader
{
    public class LoaderAllTest
    {
        [Fact]
        public void LoadAllCharactersTest()
        {
            var characters = new CharacterPreset().LoadAll();
            Assert.True(characters.Count(x => x.Id == 1) > 0);
            var exampleLoadedCharacter = characters.First().Component.Value;
            Assert.IsType<Character>(exampleLoadedCharacter);
            Assert.True(exampleLoadedCharacter.Id == 1);
        }

        [Fact]
        public void LoadAllBackgroundsTest()
        {
            var backgrounds = new BackgroundPreset().LoadAll();
            var sampleBackgrounds = backgrounds.Where(x => x.Id == 105);
            Assert.True(sampleBackgrounds.Count() > 0);
            var background = sampleBackgrounds.First().Component.Value;
            Assert.IsType<Background>(background);
            Assert.True(background.Id == 105);
        }
    }
}
