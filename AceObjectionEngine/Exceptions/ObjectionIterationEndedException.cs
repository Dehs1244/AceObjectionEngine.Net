using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Exceptions
{
    public class ObjectionIterationEndedException : ObjectionException
    {
        public ObjectionIterationEndedException(int iteration) : base($"Iteration {iteration} is ended")
        {

        }
    }
}
