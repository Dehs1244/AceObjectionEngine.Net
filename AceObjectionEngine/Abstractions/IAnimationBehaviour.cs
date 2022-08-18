using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Abstractions
{
    /// <summary>
    /// Implements methods for animating objects on a frame
    /// </summary>
    public interface IAnimationBehaviour
    {
        ISpriteSource[] Use(int index);
    }
}
