using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using AceObjectionEngine.Engine.AudioMixers;
using AceObjectionEngine.Engine.Model;
using AceObjectionEngine.Abstractions;

namespace AceObjectionEngine.Tests.Engine
{
    public class AudioMixersTest
    {
        [Theory]
        [InlineData(1)]
        [InlineData(4)]
        public void CreateAudioMixer(int seconds)
        {
            var mixer = new FFMpegAudioMixer(TimeSpan.FromSeconds(seconds));
            var file = $"test{seconds}s.mp3";
            mixer.Save(file);
            Assert.True(File.Exists(file));
        }

        [Theory]
        [InlineData(5)]
        public void MergeAudio(int seconds)
        {
            var mixer = new FFMpegAudioMixer(TimeSpan.FromSeconds(seconds));
            IAudioSource testAudio = AudioSource.FromFile(TestResources.Mp3Audio1);
            mixer.Merge(ref testAudio);
            mixer.Save("mergedAudio.mp3");
            testAudio.Dispose();
            Assert.True(File.Exists("mergedAudio.mp3"));
        }

        [Theory]
        [InlineData(5)]
        public void MixAudio(int seconds)
        {
            var mixer = new FFMpegAudioMixer(TimeSpan.FromSeconds(seconds));
            IAudioSource testAudio = AudioSource.FromFile(TestResources.Mp3Audio1);
            mixer.Mix(ref testAudio);
            mixer.Save("mixedAudio.mp3");
            testAudio.Dispose();
            Assert.True(File.Exists("mixedAudio.mp3"));
        }
    }
}
