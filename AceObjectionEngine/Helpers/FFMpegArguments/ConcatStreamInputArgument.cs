using FFMpegCore.Arguments;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AceObjectionEngine.Helpers.FFMpegArguments
{
    internal class ConcatStreamInputArgument : IInputArgument
    {
        private ICollection<StreamInputArgument> _writersInput;

        public string Text => $"-i \"concat:{string.Join(@"|", _writersInput.Select(x => x.Path))}\"";

        public ConcatStreamInputArgument(IEnumerable<Stream> writers)
        {
            _writersInput = writers.Select(x=> new StreamInputArgument(x)).ToList();
        }

        public async Task During(CancellationToken cancellationToken = default)
        {
            foreach(var writer in _writersInput)
            {
                await writer.During();
            }
        }

        public void Post()
        {
            foreach (var writer in _writersInput)
            {
                writer.Post();
            }
        }

        public void Pre()
        {
            foreach (var writer in _writersInput)
            {
                writer.Pre();
            }
        }
    }
}
