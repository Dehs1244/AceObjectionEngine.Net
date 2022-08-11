using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AceObjectionEngine.Engine.Enums.SafetyEnums
{
    /// <summary>
	/// Enum analog.
	/// </summary>
	/// <typeparam name="T"> Heir </typeparam>
	[Serializable]
    public class SafetyEnum<T> : IEqualityComparer<SafetyEnum<T>>, IEquatable<SafetyEnum<T>>
            where T : SafetyEnum<T>, new()
    {
        private string _value;


        public bool Equals(SafetyEnum<T> x, SafetyEnum<T> y) => x == y;

        public bool Equals(SafetyEnum<T> other) => Equals(this, other);

        public int GetHashCode(SafetyEnum<T> obj) => obj._value.GetHashCode();

        public static T RegisterValue(string value) => new T { _value = value };
		public static T Combine(params SafetyEnum<T>[] values) => new T { _value = values.Select(x => x._value).Aggregate((x, y) => x.ToString() + y.ToString()) };

        public override string ToString() => _value;

		public static bool operator ==(SafetyEnum<T> a, SafetyEnum<T> b)
		{
			if (ReferenceEquals(a, b))
			{
				return true;
			}

			if (a is null || b is null)
				return false;

			return a._value == b._value;
		}

		public static bool operator !=(SafetyEnum<T> a, SafetyEnum<T> b) => !(a == b);

		public static T operator +(SafetyEnum<T> a, SafetyEnum<T> b) => Combine(a, b);

		public override bool Equals(object obj) => this == (SafetyEnum<T>) obj;

		public override int GetHashCode() => GetHashCode(this);
    }
}
