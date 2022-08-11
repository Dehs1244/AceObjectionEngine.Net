using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Engine.Attributes
{
    public class ObjectionOptionRangeAttribute : BaseOptionAttribute
    {
        private float _minimal;
        private float _maximal;

        public override Type[] ForTypes => new Type[] { typeof(float), typeof(decimal), typeof(double) };

        public ObjectionOptionRangeAttribute(float maximal, bool regulation = false)
        {
            _minimal = 0;
            _maximal = maximal;
            AutomaticRegulation = regulation;
        }

        public ObjectionOptionRangeAttribute(float minimal, float maximal, bool regulation = false)
        {
            _minimal = minimal;
            _maximal = maximal;
            AutomaticRegulation = regulation;
        }

        public ObjectionOptionRangeAttribute(double maximal, bool regulation = false)
        {
            _minimal = 0;
            _maximal = (float)maximal;
            AutomaticRegulation = regulation;
        }

        public ObjectionOptionRangeAttribute(double minimal, double maximal, bool regulation = false)
        {
            _minimal = (float)minimal;
            _maximal = (float)maximal;
            AutomaticRegulation = regulation;
        }

        public bool IsInRange(float value) => value >= _minimal && value <= _maximal;
        public float OptionalValue(float value) => value < _minimal ? _minimal : _maximal;

        public override object OptionalValue(object value) => OptionalValue((float)value);

        public override bool IsValidate(object value) => IsInRange((float)value);
    }
}
