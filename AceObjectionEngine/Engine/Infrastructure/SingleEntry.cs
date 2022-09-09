using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Engine.Infrastructure
{
    public struct SingleEntry<T>
    {
        private T _value;
        private bool _hasValue;

        public SingleEntry(T value)
        {
            _value = value;
            _hasValue = true;
        }

        public override string ToString()
        {
            return _hasValue ? _value.ToString() : "";
        }

        public T Value
        {
            get
            {
                if (!_hasValue) throw new InvalidOperationException("Value not set");
                return _value;
            }
            set
            {
                if (_hasValue) throw new InvalidOperationException("Value already set");
                _value = value;
                _hasValue = true;
            }
        }

        public bool IsValueCreated => _hasValue;

        public T ValueOrDefault => _hasValue ? _value : default;

        public static implicit operator T(SingleEntry<T> value) => value.Value;

        public static implicit operator SingleEntry<T>(T value) => new SingleEntry<T>(value);
    }
}
