using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Abstractions;
using AceObjectionEngine.Engine.Attributes;
using AceObjectionEngine.Exceptions;

namespace AceObjectionEngine.Engine.Utils
{
    public static class ObjectionSettingHelpers
    {
        public static void ApplyAttributesOfOptions<T>(IObjectionSettings<T> settings) where T : class, IObjectionObject
        {
            var typeOfSettings = settings.GetType();
            foreach (var field in typeOfSettings.GetFields())
            {
                var fieldValue = field.GetValue(settings);
                var attributes = field.GetCustomAttributes<BaseOptionAttribute>(true);
                if (attributes.Any())
                {
                    foreach (var optionAttribute in attributes)
                    {
                        if (optionAttribute.IsCanApplyFor(fieldValue.GetType()))
                        {
                            if (optionAttribute.IsValidate(fieldValue) &&
                                optionAttribute.AutomaticRegulation) field.SetValue(settings, optionAttribute.OptionalValue(fieldValue));
                            else throw new ObjectionNotValidateOptionException<T>(settings);
                        }
                    }
                }
            }
        }

    }
}
