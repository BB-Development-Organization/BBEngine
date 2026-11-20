using System;
using System.Runtime.Serialization;

namespace BBEngine.Math
{
    [DataContract]
    public struct intnum : IEquatable<intnum>, IComparable<intnum>
    {
        [DataMember]
        public int value { get; private set; }

        public intnum(int value)
        {
            this.value = value;
        }

        // ===================================
        // Implicit Conversion
        // ===================================

        // int -> intnum
        public static implicit operator intnum(int value)
            => new(value);

        // intnum -> int
        public static implicit operator int(intnum value)
            => value.value;

        // intnum -> string
        public static implicit operator string(intnum value)
            => value.value.ToString();

        // ===================================
        // Arithmetic Operators
        // ===================================

        public static intnum operator +(intnum a, intnum b)
            => new(a.value + b.value);

        public static intnum operator -(intnum a, intnum b)
            => new(a.value - b.value);

        public static intnum operator *(intnum a, intnum b)
            => new(a.value * b.value);

        public static intnum operator /(intnum a, intnum b)
            => new(a.value / b.value);

        public static intnum operator %(intnum a, intnum b)
            => new(a.value % b.value);

        public static intnum operator -(intnum a)
            => new(-a.value);

        public static intnum operator +(intnum a)
            => a;

        // ===================================
        // Comparison Operators
        // ===================================

        public static bool operator ==(intnum a, intnum b)
            => a.value == b.value;

        public static bool operator !=(intnum a, intnum b)
            => a.value != b.value;

        public static bool operator >(intnum a, intnum b)
            => a.value > b.value;

        public static bool operator <(intnum a, intnum b)
            => a.value < b.value;

        public static bool operator >=(intnum a, intnum b)
            => a.value >= b.value;

        public static bool operator <=(intnum a, intnum b)
            => a.value <= b.value;

        // ===================================
        // Increment / Decrement
        // ===================================

        public static intnum operator ++(intnum a)
            => new(a.value + 1);

        public static intnum operator --(intnum a)
            => new(a.value - 1);

        // ===================================
        // Equality
        // ===================================

        public bool Equals(intnum other)
            => value == other.value;

        public override bool Equals(object? obj)
            => obj is intnum other && Equals(other);

        public override int GetHashCode()
            => value.GetHashCode();

        // ===================================
        // Comparison
        // ===================================

        public int CompareTo(intnum other)
            => value.CompareTo(other.value);

        // ===================================
        // Utilities
        // ===================================

        public override string ToString()
            => value.ToString();

        public static intnum Abs(intnum a)
            => new(System.Math.Abs(a.value));

        public static intnum Min(intnum a, intnum b)
            => new(System.Math.Min(a.value, b.value));

        public static intnum Max(intnum a, intnum b)
            => new(System.Math.Max(a.value, b.value));
    }
}