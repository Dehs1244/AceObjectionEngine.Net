using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Abstractions;

namespace AceObjectionEngine.Tests.TestObjects
{
    public sealed class TestAudioSource : IAudioSource
    {
        public TimeSpan Offset => throw new NotImplementedException();

        public string FilePath => throw new NotImplementedException();

        public Stream Stream => throw new NotImplementedException();

        public string Codec => throw new NotImplementedException();

        public long BitRate => throw new NotImplementedException();

        public string Format => throw new NotImplementedException();

        public TimeSpan Duration => throw new NotImplementedException();

        public bool IsFixate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public IAudioSource AddOffset(TimeSpan offset)
        {
            throw new NotImplementedException();
        }

        public object Clone()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IAudioSource Merge(IAudioSource another)
        {
            throw new NotImplementedException();
        }

        public IAudioSource Mix(IEnumerable<IAudioSource> audioSources)
        {
            throw new NotImplementedException();
        }

        public void Save(string path)
        {
            throw new NotImplementedException();
        }

        public IAudioSource Series(int loopTime)
        {
            throw new NotImplementedException();
        }

        public IAudioSource SetDuration(TimeSpan duration)
        {
            throw new NotImplementedException();
        }
    }
}
