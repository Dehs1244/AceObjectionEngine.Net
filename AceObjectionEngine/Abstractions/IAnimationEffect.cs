using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Abstractions
{
    /// <summary>
    /// Interface for implementing animation effects
    /// </summary>
    public interface IAnimationEffect
    {
        /// <summary>
        /// Applying an effect to a <see cref="ISpriteSource"/>
        /// </summary>
        /// <param name="sprite">Sprite foundation for animation</param>
        /// <returns>New animated <see cref="ISpriteSource"/> with effect</returns>
        ISpriteSource Implement(ISpriteSource sprite);
    }
}
