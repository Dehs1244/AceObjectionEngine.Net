using AceObjectionEngine.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Engine.Infrastructure;

namespace AceObjectionEngine.Engine.Model
{
    public class AceAudioTrackContainer
    {
        public IAudioSource Audio;
        public TimeSpan Start;
        public TimeSpan End;

        public AceAudioTrackContainer(IAudioSource audio)
        {
            Audio = audio;
        }
    }
}
