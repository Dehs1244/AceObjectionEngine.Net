using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Abstractions;

namespace AceObjectionEngine.Examples.ImplementationExamples
{
    internal class AudioSourceImplementationExample : IAudioSource
    {
        public TimeSpan Offset { get; }

        public string FilePath { get; }

        public Stream Stream { get; }

        public string Codec { get; }

        public long BitRate { get; }

        public string Format { get; }

        public TimeSpan Duration { get; }

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
