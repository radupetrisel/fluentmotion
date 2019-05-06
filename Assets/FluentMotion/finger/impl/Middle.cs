using System;
using FluentMotion.helpers;
using Leap;
using Leap.Unity;
using Leap.Unity.Infix;
using UnityEngine;

namespace FluentMotion.finger.impl
{
    public class Middle
    {
        public static bool IsExtended(Hand hand) => hand.GetMiddle().IsExtended;

        public static Func<Hand, bool> IsPointingTo<TTarget>(TTarget target)
            where TTarget : Component => hand => hand.GetMiddle()
                                                     .Direction
                                                     .ToVector3()
                                                     .normalized
                                                     .Dot(target.transform.position.normalized)
                                                     .IsAlmostEqualTo(1, 0.075f);
    }
}