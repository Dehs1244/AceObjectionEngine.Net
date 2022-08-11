using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Exceptions
{
    public class ObjectionMinimalSizeException : ObjectionException
    {
        public ObjectionMinimalSizeException(int requireSize, int size) : base($"Animator requires minimum size {requireSize}, but it's {size}")
        {
        }
    }
}
