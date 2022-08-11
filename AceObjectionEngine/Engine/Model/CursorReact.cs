using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Engine.Model
{
    /// <summary>
    /// An area targettable by the cursor in the "point to an area" case action.
    /// </summary>
    public struct CursorReact
    {
        public int Left;
        public int Top;
        public int Width;
        public int Height;
    }
}
