using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Exceptions
{
    public class ObjectionReadingJsonException : ObjectionException
    {
        public ObjectionReadingJsonException(object key) : base($"Json is invalid (does not contain {key})")
        {
        }
    }
}
