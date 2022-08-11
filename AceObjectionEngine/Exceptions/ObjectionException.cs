using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Exceptions
{
    public class ObjectionException : Exception
    {
        public ObjectionException(string message) : base($"Objection Engine: {message}")
        {

        }
    }
}
