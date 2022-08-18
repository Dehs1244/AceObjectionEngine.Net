using AceObjectionEngine.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Exceptions
{
    public class ObjectionVisorObjectException<T> : ObjectionException
    {
        public ObjectionVisorObjectException() : base($"Object {typeof(T).Name} can not be loaded, because it has invalid data")
        {

        }

        public ObjectionVisorObjectException(string path) : base($"Object {typeof(T).Name} can not be loaded, because it has invalid data. Path: " + $@"{path}")
        {

        }
    }
}
