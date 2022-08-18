using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FFMpegCore.Arguments;
using FFMpegCore.Pipes;

namespace AceObjectionEngine.Helpers.FFMpegArguments
{
    internal class ConcatPipeInputArgument : IInputArgument
    {
        private IReadOnlyCollection<PipeArgument> _pipes;

        public ConcatPipeInputArgument(IEnumerable<IPipeSource> pipes)
        {
            _pipes = pipes.Select(x => new InputPipeArgument(x)).ToList();
        }

        public string Text => $"-i \"concat:{string.Join(@"|", _pipes.Select(x=> x.PipePath))}\"";

        public async Task During(CancellationToken cancellationToken = default)
        {
            foreach(var pipe in _pipes)
            {
                await pipe.During(cancellationToken);
            }
        }

        public void Post()
        {
            foreach(var pipe in _pipes)
            {
                pipe.Post();
            }
        }

        public void Pre()
        {
            foreach (var pipe in _pipes)
            {
                pipe.Pre();
            }
        }
    }
}
