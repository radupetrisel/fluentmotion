using System;

namespace FluentMotion.helpers
{
    public static class Mixins
    {
        public static float Degrees(this float @this) => (float) (@this * 180.0f  / Math.PI);
        public static float Radians(this float @this) => (float) (@this * Math.PI / 180.0f);
    }
}