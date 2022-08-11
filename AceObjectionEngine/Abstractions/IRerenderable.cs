using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Abstractions
{
    public interface IRerenderable
    {
        bool IsNeedReRender { get; set; }
    }
}
