using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Engine.Enums.SafetyEnums
{
    public sealed class Groups : SafetyEnum<Groups>
    {
        public static Groups Normal => RegisterValue("n");
        public static Groups Ce => RegisterValue("ce");
        public static Groups GameOver => RegisterValue("go");
    }
}
