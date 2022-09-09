using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AceObjectionEngine.Abstractions;
using System.Threading.Tasks;

namespace AceObjectionEngine.Engine.Animator
{
    public class FrameFragment : IFrameFragment
    {
        public string FilePath { get; }

        public FrameFragment(string filePath)
        {
            FilePath = filePath;
        }

        public Stream GetStream() => File.OpenRead(FilePath);

        public void Dispose()
        {
            File.Delete(FilePath);
        }
    }
}
