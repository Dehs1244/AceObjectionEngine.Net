using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FFMpegCore;
using FFMpegCore.Pipes;
using FFMpegCore.Arguments;
using AceObjectionEngine.Helpers.FFMpegArguments;

namespace AceObjectionEngine.Helpers
{
    internal static partial class FFMpegHelper
    {
        public static FFMpegCore.FFMpegArguments FromSilenceInput(Action<FFMpegArgumentOptions> addArguments = null)
            => FFMpegCore.FFMpegArguments.FromDeviceInput("anullsrc=channel_layout=5.1:sample_rate=48000", addArguments);

        public static FFMpegCore.FFMpegArguments WithInput(IInputArgument input, Action<FFMpegArgumentOptions> addArguments = null)
            => (FFMpegCore.FFMpegArguments)Activator.CreateInstance(typeof(FFMpegCore.FFMpegArguments), input, addArguments);

        public static FFMpegCore.FFMpegArguments FromConcatPipeInput(IEnumerable<IPipeSource> pipes, Action<FFMpegArgumentOptions> addArguments = null)
            => WithInput(new ConcatPipeInputArgument(pipes), addArguments);
    }
}
