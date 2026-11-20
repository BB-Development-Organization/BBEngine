namespace BBEngine.Math
{
    public static class math
    {
        public static readonly dotnum INF = double.PositiveInfinity;
        public static readonly dotnum NEG_INF = double.NegativeInfinity;
        public static readonly dotnum NaN = double.NaN;

        public static readonly dotnum PI = System.Math.PI;
        public static readonly dotnum TAU = System.Math.Tau;

        public static readonly dotnum E = System.Math.E;

        public static readonly dotnum DEG_TO_RAD = PI / 180.0d;
        public static readonly dotnum RAD_TO_DEG = 180.0d / PI;
    }
}