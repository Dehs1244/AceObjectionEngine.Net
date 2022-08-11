using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Engine.Model
{
    public struct ObjectionColor
    {
        private Color _color;
        public Color Representive => _color;

        //Must use hex format, either #aaa or #ababab.
        public ObjectionColor(string hex)
        {
            _color = ColorTranslator.FromHtml(hex);
        }

        /// <summary>
        /// Convert <see cref="System.Drawing.Color"/> to ObjectionColor
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        public static ObjectionColor FromColor(Color color) => new ObjectionColor(ColorTranslator.ToHtml(color));
    }
}
