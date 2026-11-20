namespace BBEngine.Math
{
    public struct transform
    {
        /// <summary>
        /// Position of the transform
        /// </summary>
        public vec2 position { get; set; }

        /// <summary>
        /// Scale of the transform
        /// </summary>
        public vec2 scale { get; set; }

        /// <summary>
        /// degrees angle of the transform
        /// </summary>
        public float angle { get; set; }

        public transform(vec2 position, vec2 scale)
        {
            this.position = position;
            this.scale = scale;
            angle = 0;
        }

        public transform(vec2 position, vec2 scale, float angle)
        {
            this.position = position;
            this.scale = scale;
            this.angle = angle;
        }

        // parent-child sorting operator
        public static transform operator >>>(transform a, transform b) => new(a.position + b.position, a.scale * b.scale, a.angle + b.angle);
    }
}
