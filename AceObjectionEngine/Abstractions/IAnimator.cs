using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Abstractions.Async;

namespace AceObjectionEngine.Abstractions
{
    public interface IAnimator<T> : IAnimatorAsync<T>, IDisposable
         where T : class, IAnimatorHierarchy<T>
    {
        /// <summary>
        /// Animator Width
        /// </summary>
        int Width { get; }
        /// <summary>
        /// Animator Height
        /// </summary>
        int Height { get; }
        /// <summary>
        /// Sample layer render
        /// </summary>
        IFrameRenderer<T> Renderer { get; }
        /// <summary>
        /// Animation start method frame hierarchy
        /// </summary>
        void Animate();
    }
}
