using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Abstractions;
using AceObjectionEngine.Engine.Model;

namespace AceObjectionEngine.Engine.MediaMakers
{
    public class DefaultAudioSourceMaker : IMediaMaker<IAudioSource>
    {
        public IAudioSource Make(string filePath) => AudioSource.FromFile(filePath);

        public IAudioSource Make(string filePath, params object[] args)
        {
            if (args[1] is TimeSpan offset)
            {
                return AudioSource.FromFile(filePath, offset);
            }

            return Make(filePath);
        }
    }
}
