using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Abstractions.Async;
using AceObjectionEngine.Engine.Model;

namespace AceObjectionEngine.Abstractions
{
    public interface IAnimationObject : IAnimationObjectAsync
    {
        /// <summary>
        /// Called at each end of the animation
        /// </summary>
        void EndAnimation();
    }
}
