using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Engine.Enums.SafetyEnums
{
    public class Filters : SafetyEnum<Filters>
    {
        public Filters GrayScale => RegisterValue("grayscale");
        public Filters Invert => RegisterValue("invert");
        public Filters Sepia => RegisterValue("invert");
        public Filters HueRotate => RegisterValue("hue-rotate");
    }
}
