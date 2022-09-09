using AceObjectionEngine.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Engine.Model
{
    public struct AceComponentSpan<T> where T : IObjectionObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Lazy<T> Component { get; set; }
    }
}
