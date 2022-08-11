using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Engine.Model
{
    public struct AceSize
    {
        public int Width { get; }
        public int Height { get; }

        public AceSize(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}
