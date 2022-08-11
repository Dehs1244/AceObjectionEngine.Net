using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Exceptions
{
    internal class ObjectionConverterException : ObjectionException
    {
        public ObjectionConverterException(object from, object to) : base($"Object {from} can not be converted into Objection Object {to}")
        {

        }
    }
}
