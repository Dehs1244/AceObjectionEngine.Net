using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Abstractions;
using AceObjectionEngine.Engine.AudioMixers;

namespace AceObjectionEngine.Examples.ImplementationExamples
{
    public class ExampleAudioMixer : AudioMixer
    {
        public ExampleAudioMixer(TimeSpan timeLine) : base(timeLine)
        {
        }

        public override object Clone()
        {
            throw new NotImplementedException();
        }

        public override IAudioSource Create()
        {
            throw new NotImplementedException();
        }

        public override void CreateTimeLine()
        {
            throw new NotImplementedException();
        }

        public override void Merge(ref IAudioSource audioSource)
        {
            throw new NotImplementedException();
        }

        public override void Mix(ref IAudioSource audioSource)
        {
            throw new NotImplementedException();
        }
    }
}
