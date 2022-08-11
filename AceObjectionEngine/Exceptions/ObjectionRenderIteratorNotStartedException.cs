using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Exceptions
{
    public class ObjectionRenderIteratorNotStartedException : ObjectionException
    {
        public ObjectionRenderIteratorNotStartedException() : base("Render iteration not started to use it")
        {

        }
    }
}
