using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace AceObjectionEngine.Helpers
{
    internal static class TypeHelper
    {
        public static T GetExtensiveAttribute<T>(Type type, bool inherit)
            where T : Attribute 
            => type.GetCustomAttribute<T>(inherit) ?? type.GetInterfaces().Select(x => GetExtensiveAttribute<T>(x, inherit))
            .Where(x=> x != null).FirstOrDefault();

        public static T GetExtensiveAttribute<T>(Type type)
            where T : Attribute
            => GetExtensiveAttribute<T>(type, false);

        public static T GetExtensiveAttribute<T>(object obj)
            where T : Attribute
            => GetExtensiveAttribute<T>(obj.GetType(), false);

        public static T GetExtensiveAttribute<T>(object obj, bool inherit)
            where T : Attribute
            => GetExtensiveAttribute<T>(obj.GetType(), false);
    }
}
