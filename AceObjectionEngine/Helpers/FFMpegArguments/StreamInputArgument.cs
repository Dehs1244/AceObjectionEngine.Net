using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FFMpegCore.Arguments;

namespace AceObjectionEngine.Helpers.FFMpegArguments
{
    internal class StreamInputArgument : IInputArgument
    {
        private TempFileStream _temp;
        public string Text => $"-i \"{_temp.Name}\"";
        public Stream Writer { get; }

        public string Path => _temp.FilePath;

        public StreamInputArgument(Stream writer)
        {
            Writer = writer;
        }

        public Task During(CancellationToken cancellationToken = default) => Task.CompletedTask;

        public void Post()
        {
            _temp.Dispose();
        }

        public void Pre()
        {
            _temp = new TempFileStream($"outWriter");
            Writer.Seek(0, SeekOrigin.Begin);
            Writer.CopyTo(_temp);
            _temp.Close();
        }
    }
}
