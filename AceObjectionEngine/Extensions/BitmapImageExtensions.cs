using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Engine.Enums;

namespace AceObjectionEngine.Extensions
{
    internal static class BitmapImageExtensions
    {
        public static void SaveAsGif(this Image img, Stream stream)
        {
            img.Save(stream, ImageFormat.Gif);
        }
    }
}
