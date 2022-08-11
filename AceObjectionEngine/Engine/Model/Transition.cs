using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Engine.Model
{
    public class Transition
    {
        public int Duration;
        public Enums.SafetyEnums.Easings Easing = Enums.SafetyEnums.Easings.Linear;
    }
}
