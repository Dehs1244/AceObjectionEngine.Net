using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Abstractions.Async
{
    public interface IObjectionPresetAsync
    {
        string Path { get; }
        int Id { get; set; }

        Task<IObjectionObject> LoadAsync();
    }
}
