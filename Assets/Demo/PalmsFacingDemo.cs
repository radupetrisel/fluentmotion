using Assets.FluentMotion.hand;
using Leap.Unity;
using System;
using UniRx;
using UnityEngine;
using LeapVector = Leap.Vector;

namespace Assets.Demo
{
    public sealed class PalmsFacingDemo : ReactiveHandsDetector
    {
        private static bool Around(double angle, double angleToCompareTo)
        {
            return Math.Abs(angle - angleToCompareTo) < Math.PI / 6.0;
        }

        public override void Detect()
        {
            Hands
                .Where(hands => Around(hands.LeftHand.PalmNormal.Normalized.AngleTo(hands.RightHand.PalmNormal.Normalized), Math.PI))
                .Subscribe(hands => Debug.DrawLine(hands.LeftHand.StabilizedPalmPosition.ToVector3(), hands.RightHand.StabilizedPalmPosition.ToVector3(), Color.red, 1000));
        }
    }
}
