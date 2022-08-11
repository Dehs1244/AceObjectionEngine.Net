using AceObjectionEngine.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Exceptions
{
    public sealed class ObjectionNotValidateOptionException<T> : ObjectionException where T : class, IObjectionObject
    {
        public ObjectionNotValidateOptionException(IObjectionSettings<T> settings) : base($"options in the settings \"{nameof(settings)}\" are incorrect")
        {
        }
    }
}
