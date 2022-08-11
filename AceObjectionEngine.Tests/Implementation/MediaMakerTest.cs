using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using AceObjectionEngine.Tests.TestObjects;
using AceObjectionEngine.Engine.Infrastructure;
using AceObjectionEngine.Engine.MediaMakers;
using AceObjectionEngine.Tests.TestObjects.TestMakers;

namespace AceObjectionEngine.Tests.Implementation
{
    public class MediaMakerTest
    {
        [Fact]
        public void SetTestSpriteTest()
        {
            MediaMaker.SetSpriteMaker(() => new TestSpriteMaker());
            Assert.IsType<TestSpriteMaker>(MediaMaker.SpriteMaker);
        }

        [Fact]
        public void SetTestAudioSourceTest()
        {
            MediaMaker.SetAudioSourceMaker(() => new TestAudioSourceMaker());
            Assert.IsType<TestAudioSourceMaker>(MediaMaker.AudioSourceMaker);
        }

        [Fact]
        public void MakeSpriteTest()
        {
            MediaMaker.SetSpriteMaker(() => new TestSpriteMaker());
            Assert.IsType<TestSpriteMaker>(MediaMaker.SpriteMaker);
            var makedSprite = MediaMaker.SpriteMaker.Make(string.Empty);
            Assert.IsType<TestSprite>(makedSprite);
        }

        [Fact]
        public void MakeAudioSourceTest()
        {
            MediaMaker.SetAudioSourceMaker(() => new TestAudioSourceMaker());
            Assert.IsType<TestAudioSourceMaker>(MediaMaker.AudioSourceMaker);
            var makedAudioSource = MediaMaker.AudioSourceMaker.Make(string.Empty);
            Assert.IsType<TestAudioSource>(makedAudioSource);
        }
    }
}
