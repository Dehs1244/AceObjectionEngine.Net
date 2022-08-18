using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Exceptions
{
    public class ObjectionuNotSupportedTypeOperationException<T> : ObjectionException
    {
        public ObjectionuNotSupportedTypeOperationException(object consumer, string operation) 
            : base($"Object {nameof(T)} not supporting type {nameof(consumer)} for operation {operation}")
        {
        }
    }
}
