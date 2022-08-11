using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AceObjectionEngine.Abstractions
{
    public interface IAudioSource : ICloneable, IDisposable, IAnimationMedia
    {
        TimeSpan Offset { get; }
        string FilePath { get; }
        Stream Stream { get; }

        string Codec { get; }
        long BitRate { get; }

        /// <summary>
        /// Merging another sound source and returning a new merged instance of the sound source
        /// </summary>
        /// <param name="another"><see cref="IAudioSource"/> which will be merged</param>
        /// <returns>New Merged Instance Of <see cref="IAudioSource"/></returns>
        IAudioSource Merge(IAudioSource another);
        /// <summary>
        /// Mixing enumerable of sounds source and returning a new merged instance of the sound source
        /// </summary>
        /// <param name="another"><see cref="IAudioSource"/> which will be mixing</param>
        /// <returns>New Mixed Instance Of <see cref="IAudioSource"/></returns>
        IAudioSource Mix(IEnumerable<IAudioSource> audioSources);
        /// <summary>
        /// Loops audio source
        /// </summary>
        /// <param name="loopTime">Time for loop</param>
        /// <returns>New Looped Instance Of <see cref="IAudioSource"/></returns>
        IAudioSource Series(int loopTime);
        /// <summary>
        /// Set New Duration For audio and returning a new audio
        /// If the duration is longer than original, it adds silence, if less, it cuts off
        /// </summary>
        /// <param name="duration">New audio duration</param>
        /// <returns>New Instance Of <see cref="IAudioSource"/> with new duration</returns>
        IAudioSource SetDuration(TimeSpan duration);
        void Save(string path);
    }
}
