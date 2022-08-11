using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Abstractions;

namespace AceObjectionEngine.Tests.TestObjects.TestMakers
{
    public sealed class TestAudioSourceMaker : IMediaMaker<IAudioSource>
    {
        public IAudioSource Make(string filePath) => new TestAudioSource();

        public IAudioSource Make(string filePath, params object[] args) => new TestAudioSource();
    }
}
