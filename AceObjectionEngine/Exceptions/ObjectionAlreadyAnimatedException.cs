using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Exceptions
{
    public class ObjectionAlreadyAnimatedException : ObjectionException
    {
        public ObjectionAlreadyAnimatedException() : base("Animator already animated the frames, it is not possible to animate one animator at the same time") 
        {
        }
    }
}
