using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Abstractions
{
    /// <summary>
    /// Audio Mixer Interface
    /// </summary>
    public interface IAudioMixer : ICloneable
    {
        Stream Stream { get; }
        /// <summary>
        /// Attaches audio to the audio mixer track and can be the modify audio
        /// </summary>
        /// <param name="audioSource">Audio to Merge</param>
        void Merge(ref IAudioSource audioSource);
        /// <summary>
        /// Overlays audio onto the audio mixer track
        /// </summary>
        /// <param name="audioSource">Audio to Mix</param>
        void Mix(ref IAudioSource audioSource);
        /// <summary>
        /// Converts <see cref="IAudioMixer"/> to <see cref="IAudioSource"/>
        /// </summary>
        /// <returns>Audio Source</returns>
        IAudioSource Create();
    }
}
