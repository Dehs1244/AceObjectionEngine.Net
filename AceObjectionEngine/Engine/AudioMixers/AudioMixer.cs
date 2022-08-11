using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Abstractions;

namespace AceObjectionEngine.Engine.AudioMixers
{
    public abstract class AudioMixer : IAudioMixer, IDisposable
    {
        public Stream Stream { get; }
        protected TimeSpan Duration { get; }

        public AudioMixer(TimeSpan timeLine)
        {
            Stream = new MemoryStream();
            Duration = timeLine;
            Init();
            CreateTimeLine();
        }

        public AudioMixer(Stream stream, TimeSpan timeLine)
        {
            Stream = stream;
            Duration = timeLine;
        }

        protected virtual void Init()
        {

        }

        public abstract void CreateTimeLine();
        public abstract IAudioSource Create();
        public abstract void Merge(ref IAudioSource audioSource);
        public abstract void Mix(ref IAudioSource audioSource);

        public void Dispose()
        {
            Stream.Dispose();
        }

        public abstract object Clone();
    }
}
