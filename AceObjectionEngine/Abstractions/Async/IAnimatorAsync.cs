using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Abstractions.Async
{
    public interface IAnimatorAsync<T>
         where T : class, IAnimatorHierarchy<T>
    {
        ICollection<T> Hierarchy { get; }
        Stream OutputStream { get; }
        Task AnimateAsync();
    }
}
