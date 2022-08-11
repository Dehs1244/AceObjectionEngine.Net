using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using AceObjectionEngine.Abstractions;

namespace AceObjectionEngine.Loader.Model
{
    public struct LoaderData<T> where T : IObjectionObject
    {
        public bool Successfully;
        public T Subject;
    }
}
