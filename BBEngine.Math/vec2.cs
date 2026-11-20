using System;

namespace BBEngine.Math
{
    public struct vec2 : IEquatable<vec2>
    {
        // Constants
        public static readonly vec2 zero = new vec2(0, 0);
        public static readonly vec2 one = new vec2(1, 1);

        public static readonly vec2 up = new vec2(0, -1);
        public static readonly vec2 down = new vec2(0, 1);
        public static readonly vec2 left = new vec2(-1, 0);
        public static readonly vec2 right = new vec2(1, 0);

        public dotnum x { get; set; }
        public dotnum y { get; set; }

        // Constructors
        public vec2()
        {
            x = 0;
            y = 0;
        }

        public vec2(dotnum x, dotnum y)
        {
            this.x = x;
            this.y = y;
        }

        // Properties
        public dotnum length()
            => (dotnum)System.Math.Sqrt(length_squared());

        public dotnum length_squared()
            => x * x + y * y;

        public vec2 normalized()
        {
            dotnum len = length();

            if (len == 0)
                return zero;

            return this / len;
        }

        public dotnum angle()
            => (dotnum)System.Math.Atan2((double)y, (double)x);

        // Basic Math
        public dotnum dot(vec2 target)
            => x * target.x + y * target.y;

        public dotnum cross(vec2 target)
            => x * target.y - y * target.x;

        public vec2 perpendicular()
            => new vec2(-y, x);

        public vec2 abs()
            => new vec2(
                System.Math.Abs((double)x),
                System.Math.Abs((double)y)
            );

        public vec2 sign()
            => new vec2(
                System.Math.Sign((double)x),
                System.Math.Sign((double)y)
            );

        public vec2 floor()
            => new vec2(
                System.Math.Floor((double)x),
                System.Math.Floor((double)y)
            );

        public vec2 ceil()
            => new vec2(
                System.Math.Ceiling((double)x),
                System.Math.Ceiling((double)y)
            );

        public vec2 round()
            => new vec2(
                System.Math.Round((double)x),
                System.Math.Round((double)y)
            );

        // Distance
        public dotnum distance(vec2 target)
            => distance(this, target);

        public static dotnum distance(vec2 a, vec2 b)
            => (a - b).length();

        public dotnum distance_squared(vec2 target)
            => (this - target).length_squared();

        // Lerp
        public vec2 lerp(vec2 b, dotnum t)
            => lerp(this, b, t);

        public static vec2 lerp(vec2 a, vec2 b, dotnum t)
            => a + (b - a) * t;

        // Angle
        public dotnum angle_to(vec2 target)
            => (dotnum)System.Math.Atan2(
                (double)cross(target),
                (double)dot(target)
            );

        public dotnum angle_to_point(vec2 target)
            => (target - this).angle();

        public static dotnum angle_between(vec2 a, vec2 b)
            => a.angle_to(b);

        // Rotation
        public vec2 rotated(dotnum radians)
        {
            double cos = System.Math.Cos((double)radians);
            double sin = System.Math.Sin((double)radians);

            return new vec2(
                x * cos - y * sin,
                x * sin + y * cos
            );
        }

        public vec2 rotated_degrees(dotnum degrees)
            => rotated(degrees * (System.Math.PI / 180.0));

        // Reflection
        public vec2 reflect(vec2 normal)
            => this - 2 * dot(normal) * normal;

        public vec2 bounce(vec2 normal)
            => -reflect(normal);

        // Projection
        public vec2 project(vec2 target)
        {
            dotnum len = target.length_squared();

            if (len == 0)
                return zero;

            return target * (dot(target) / len);
        }

        public vec2 slide(vec2 normal)
            => this - normal * dot(normal);

        // Movement
        public vec2 direction_to(vec2 target)
            => (target - this).normalized();

        public vec2 move_toward(vec2 target, dotnum delta)
        {
            vec2 diff = target - this;
            dotnum len = diff.length();

            if (len <= delta || len == 0)
                return target;

            return this + diff / len * delta;
        }

        // Clamp
        public vec2 clamp(vec2 min, vec2 max)
            => new vec2(
                System.Math.Clamp((double)x, (double)min.x, (double)max.x),
                System.Math.Clamp((double)y, (double)min.y, (double)max.y)
            );

        public vec2 clamp_length(dotnum min, dotnum max)
        {
            dotnum len = length();

            if (len == 0)
                return zero;

            if (len < min)
                return normalized() * min;

            if (len > max)
                return normalized() * max;

            return this;
        }

        // Min / Max
        public static vec2 min(vec2 a, vec2 b)
            => new vec2(
                System.Math.Min((double)a.x, (double)b.x),
                System.Math.Min((double)a.y, (double)b.y)
            );

        public static vec2 max(vec2 a, vec2 b)
            => new vec2(
                System.Math.Max((double)a.x, (double)b.x),
                System.Math.Max((double)a.y, (double)b.y)
            );

        // Operators
        public static vec2 operator +(vec2 a, vec2 b)
            => new vec2(a.x + b.x, a.y + b.y);

        public static vec2 operator -(vec2 a, vec2 b)
            => new vec2(a.x - b.x, a.y - b.y);

        public static vec2 operator -(vec2 a)
            => new vec2(-a.x, -a.y);

        public static vec2 operator *(vec2 a, dotnum b)
            => new vec2(a.x * b, a.y * b);

        public static vec2 operator *(dotnum a, vec2 b)
            => b * a;

        public static vec2 operator /(vec2 a, dotnum b)
            => new vec2(a.x / b, a.y / b);

        public static vec2 operator *(vec2 a, vec2 b)
            => new vec2(a.x * b.x, a.y * b.y);

        public static vec2 operator /(vec2 a, vec2 b)
            => new vec2(a.x / b.x, a.y / b.y);

        // Equality
        public static bool operator ==(vec2 a, vec2 b)
            => a.x == b.x && a.y == b.y;

        public static bool operator !=(vec2 a, vec2 b)
            => !(a == b);

        public bool Equals(vec2 other)
            => this == other;

        public override bool Equals(object? obj)
            => obj is vec2 other && Equals(other);

        public override int GetHashCode()
            => HashCode.Combine(x, y);

        public override string ToString()
            => $"({x}, {y})";
    }
}