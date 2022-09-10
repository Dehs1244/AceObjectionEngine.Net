using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AceObjectionEngine.Abstractions
{
    /// <summary>
    /// Implementation Interface Video Fragment
    /// </summary>
    public interface IFrameFragment : IDisposable
    {
        Stream GetStream();
    }
}
