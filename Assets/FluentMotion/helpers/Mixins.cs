using System;
using Leap;

namespace FluentMotion.helpers
{
    public static class Mixins
    {
        public static bool IsAlmostEqualTo(this float @this, float other, float epsilon = float.Epsilon) => Math.Abs(@this - other) < epsilon;

        public static Func<TInput, bool> WithTolerance<TInput>(this Func<TInput, float> @this, float tolerance = float.Epsilon) =>
            input => @this(input).IsAlmostEqualTo(1, tolerance);

        public static Func<Hand, bool> WithSpeed(this Func<Hand, float> speed, float minimumSpeed = float.Epsilon) => 
            hand => speed(hand) > minimumSpeed;
    }
}