using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Engine.Enums.SafetyEnums
{
    public class Easings : SafetyEnum<Easings>
    {
        public static Easings Linear => RegisterValue("linear");
        public static Easings Spring => RegisterValue("spring");

        public static Easings Ease => RegisterValue("ease");
        public static Easings EaseIn => RegisterValue("ease-in");
        public static Easings EaseOut => RegisterValue("ease-out");
        public static Easings EaseInOut => RegisterValue("ease-in-out");

        public static Easings EaseInSine => RegisterValue("ease-in-sine");
        public static Easings EaseOutSine => RegisterValue("ease-out-sine");
        public static Easings EaseInOutSine => RegisterValue("ease-in-out-sine");

        public static Easings EaseInQuad => RegisterValue("ease-in-quad");
        public static Easings EaseOutQuad => RegisterValue("ease-out-quad");
        public static Easings EaseInOutQuad => RegisterValue("ease-in-out-quad");

        public static Easings EaseInCubic => RegisterValue("ease-in-cubic");
        public static Easings EaseOutCubic => RegisterValue("ease-out-cubic");
        public static Easings EaseInOutCubic => RegisterValue("ease-in-out-cubic");

        public static Easings EaseInQuart => RegisterValue("ease-in-quart");
        public static Easings EaseOutQuart => RegisterValue("ease-out-quart");
        public static Easings EaseInOutQuart => RegisterValue("ease-in-out-quart");

        public static Easings EaseInQuint => RegisterValue("ease-in-quint");
        public static Easings EaseOutQuint => RegisterValue("ease-out-quint");
        public static Easings EaseInOutQuint => RegisterValue("ease-in-out-quint");

        public static Easings EaseInExponential => RegisterValue("ease-in-exponential");
        public static Easings EaseOutExponential => RegisterValue("ease-out-exponential");
        public static Easings EaseInOutExponential => RegisterValue("ease-in-out-exponential");

        public static Easings EaseInCircular => RegisterValue("ease-in-circular");
        public static Easings EaseOutCircular => RegisterValue("ease-out-circular");
        public static Easings EaseInOutCircular => RegisterValue("ease-in-out-circular");
    }
}
