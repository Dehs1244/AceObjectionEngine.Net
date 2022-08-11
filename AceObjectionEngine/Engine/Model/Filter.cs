using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AceObjectionEngine.Engine.Enums;

namespace AceObjectionEngine.Engine.Model
{
    /// <summary>
    /// Color filter to a specific target. Amount attribute doesn't apply to INVERT and SEPIA types.
    /// </summary>
    public struct Filter
    {
        public Enums.SafetyEnums.Filters Type;
        public Enums.FilterTarget Target;
        public int Amount;
    }

}
