using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Helpers
{
    internal static class EnumerableHelper
    {
        public static IEnumerable<T> ToEnumerable<T>(params T[] items)
        {
            return items;
        }
    }
}
