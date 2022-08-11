using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Abstractions;

namespace AceObjectionEngine.Exceptions
{
    public class ObjectionObjectAlreadyAdded : ObjectionException
    {
        public ObjectionObjectAlreadyAdded(IObjectionObject obj) : base($"Object {nameof(obj)} already added in scene")
        {
        }
    }
}
