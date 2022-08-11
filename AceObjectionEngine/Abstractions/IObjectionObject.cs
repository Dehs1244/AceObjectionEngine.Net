using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Loader.Utils;

namespace AceObjectionEngine.Abstractions
{
    public interface IObjectionObject : IAnimationObject, IDisposable
    {
        int Id { get; }
    }
}
