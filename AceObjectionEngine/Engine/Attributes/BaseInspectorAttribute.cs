using AceObjectionEngine.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Engine.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
    public abstract class BaseInspectorAttribute : Attribute
    {
        private string[] _inspectors;

        public BaseInspectorAttribute()
        {
            _inspectors = Array.Empty<string>();
        }

        public BaseInspectorAttribute(params string[] conditionProperties)
        {
            _inspectors = conditionProperties;
        }

        public virtual bool Inspect(IAnimationObject animationObject)
        {
            var type = animationObject.GetType();
            foreach (var inspector in _inspectors)
            {
                var verificationPropertyType = type.GetProperty(inspector).GetValue(animationObject);
                if (verificationPropertyType is bool booleanProperty)
                {
                    if (booleanProperty == false) return false;
                }
            }

            return true;
        }
    }
}
