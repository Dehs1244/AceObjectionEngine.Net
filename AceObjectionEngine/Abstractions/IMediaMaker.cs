using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Abstractions
{
    public interface IMediaMaker<T> where T : IAnimationMedia
    {
        T Make(string filePath);
        T Make(string filePath, params object[] args);
    }
}
