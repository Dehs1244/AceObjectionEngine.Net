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
        int Width { get; }
        int Height { get; }
        IFrameRenderer<T> Renderer { get; }
        void Animate();
    }
}
