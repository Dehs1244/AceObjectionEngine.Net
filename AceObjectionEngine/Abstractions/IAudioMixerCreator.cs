using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Abstractions
{
    /// <summary>
    /// Audio Mixer Creator Implementation Interface
    /// </summary>
    public interface IAudioMixerCreator
    {
        /// <summary>
        /// Called every time, when need to create mixer
        /// </summary>
        /// <param name="timeLine"></param>
        /// <returns></returns>
        IAudioMixer CreateAudioMixer(TimeSpan timeLine);
    }
}
