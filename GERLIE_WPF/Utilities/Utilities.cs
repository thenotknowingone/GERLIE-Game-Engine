using System;

namespace GERLIE_WPF.Utilities
{
    public static class Class_for_math_utilities
    {
        public static float Epsilon => 0.00001f;
        public static bool Is_the_same_as(this float value, float other)
        {
            return Math.Abs(value - other) < Epsilon;
        }
        public static bool Is_the_same_as(this float? value, float? other)
        {
            if(!value.HasValue || !other.HasValue)
                return false;

            return Math.Abs(value.Value - other.Value) < Epsilon;
        }
    }
}
