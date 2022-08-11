using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Abstractions
{
    public interface IAudioMixer : ICloneable
    {
        Stream Stream { get; }
        void Merge(ref IAudioSource audioSource);
        void Mix(ref IAudioSource audioSource);
        IAudioSource Create();
    }
}
