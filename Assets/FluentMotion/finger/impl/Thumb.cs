using System;
using FluentMotion.helpers;
using Leap;
using Leap.Unity;
using Leap.Unity.Infix;
using UnityEngine;

namespace FluentMotion.finger.impl
{
    public static class Thumb
    {
        public static bool IsExtended(Hand hand) => hand.GetThumb().IsExtended;

        public static Func<Hand, bool> IsPointingTo(Vector3 target) => hand => hand.GetThumb()
            .Direction
            .ToVector3()
            .Dot(target)
            .IsAlmostEqualTo(1);
    }
}
