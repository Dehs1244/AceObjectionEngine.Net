using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Engine.Model
{
    public struct AceSizeP
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public AceSizeP(AceSize size, int widthP, int heightP)
        {
            Width = (int)Math.Round((double)(size.Width * widthP / 100));
            Height = (int)Math.Round((double)(size.Height * heightP / 100));
        }
    }
}
