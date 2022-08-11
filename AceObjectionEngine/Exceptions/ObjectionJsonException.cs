using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Exceptions
{
    public class ObjectionJsonException : ObjectionException
    {
        public ObjectionJsonException(string json) : base($"string \"{json}\" can not be converted")
        {

        }
    }
}
