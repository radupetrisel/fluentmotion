using System;
using Leap.Unity;
using UnityEngine;

namespace FluentMotion.helpers
{
    public static class Mixins
    {
        public static bool IsAlmostEqualTo(this float @this, float other, float epsilon = float.Epsilon) => Math.Abs(@this - other) < epsilon;
    }
}