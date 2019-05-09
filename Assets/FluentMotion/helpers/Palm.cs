using System;
using Leap;
using Leap.Unity;
using Leap.Unity.Infix;
using UnityEngine;

namespace FluentMotion.helpers
{
    public static class Palm
    {
        public static Func<Hand, float> IsFacing(Vector4 target) => 
            hand => hand.PalmNormal.ToVector3().normalized.Dot(target.normalized);
    }
}