using System;
using System.Runtime.Serialization;

namespace BBEngine.Math
{
    [DataContract]
    public struct dotnum : IEquatable<dotnum>, IComparable<dotnum>
    {
        [DataMember]
        public double value { get; private set; }

        public dotnum(double value)
        {
            this.value = value;
        }

        // ===================================
        // Implicit Conversion
        // ===================================

        // double -> dotnum
        public static implicit operator dotnum(double value)
            => new(value);

        // dotnum -> double
        public static implicit operator double(dotnum value)
            => value.value;

        // ===================================
        // Arithmetic Operators
        // ===================================

        public static dotnum operator +(dotnum a, dotnum b)
            => new(a.value + b.value);

        public static dotnum operator -(dotnum a, dotnum b)
            => new(a.value - b.value);

        public static dotnum operator *(dotnum a, dotnum b)
            => new(a.value * b.value);

        public static dotnum operator /(dotnum a, dotnum b)
            => new(a.value / b.value);

        public static dotnum operator %(dotnum a, dotnum b)
            => new(a.value % b.value);

        public static dotnum operator -(dotnum a)
            => new(-a.value);

        public static dotnum operator +(dotnum a)
            => a;

        // ===================================
        // Comparison Operators
        // ===================================

        public static bool operator ==(dotnum a, dotnum b)
            => a.value == b.value;

        public static bool operator !=(dotnum a, dotnum b)
            => a.value != b.value;

        public static bool operator >(dotnum a, dotnum b)
            => a.value > b.value;

        public static bool operator <(dotnum a, dotnum b)
            => a.value < b.value;

        public static bool operator >=(dotnum a, dotnum b)
            => a.value >= b.value;

        public static bool operator <=(dotnum a, dotnum b)
            => a.value <= b.value;

        // ===================================
        // Increment / Decrement
        // ===================================

        public static dotnum operator ++(dotnum a)
            => new(a.value + 1);

        public static dotnum operator --(dotnum a)
            => new(a.value - 1);

        // ===================================
        // Equality
        // ===================================

        public bool Equals(dotnum other)
            => value.Equals(other.value);

        public override bool Equals(object? obj)
            => obj is dotnum other && Equals(other);

        public override int GetHashCode()
            => value.GetHashCode();

        // ===================================
        // Utilities
        // ===================================

        public double toDouble()
            => value;

        public override string ToString()
            => value.ToString();

        public static dotnum Abs(dotnum a)
            => new(System.Math.Abs(a.value));

        public static dotnum Min(dotnum a, dotnum b)
            => new(System.Math.Min(a.value, b.value));

        public static dotnum Max(dotnum a, dotnum b)
            => new(System.Math.Max(a.value, b.value));

        public int CompareTo(dotnum other)
            => value.CompareTo(other.value);
    }
}