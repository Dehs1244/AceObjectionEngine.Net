using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Generators.Interfaces
{
    internal interface ISourceGeneratorAsync
    {
        string Extension { get; }
        string Name { get; }

        Task GenerateAsync();
        Task SaveAsFileAsync();
    }
}
