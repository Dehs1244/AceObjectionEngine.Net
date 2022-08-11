using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace AceObjectionEngine.Engine.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public abstract class BaseOptionAttribute : Attribute
    {
        public abstract Type[] ForTypes { get; }
        public bool AutomaticRegulation;

        public abstract object OptionalValue(object value);
        public abstract bool IsValidate(object value);

        public bool IsCanApplyFor(Type type) => ForTypes.Any(x=>x == type);
    }
}
