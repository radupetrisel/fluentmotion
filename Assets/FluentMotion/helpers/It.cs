using System;
using System.Collections.Generic;
using System.Linq;
using FluentMotion.helpers.hand;
using Leap;
using Leap.Unity;
using Leap.Unity.Infix;
using TMPro.EditorUtilities;
using UnityEngine;
using Valve.VR.InteractionSystem.Sample;

namespace FluentMotion.helpers
{
    public static class It
    {
        private const float LeapTolerance = 0.2f;

        public static bool IsExtended(Finger finger) => finger.IsExtended;
        public static bool IsNotExtended(Finger finger) => !finger.IsExtended;
        
        public static Finger GetThumb(Hand  hand) => hand.GetThumb();
        public static Finger GetIndex(Hand  hand) => hand.GetIndex();
        public static Finger GetMiddle(Hand hand) => hand.GetMiddle();
        public static Finger GetRing(Hand   hand) => hand.GetRing();
        public static Finger GetPinky(Hand  hand) => hand.GetPinky();

        public static Func<Hand, Finger> GetFinger(Finger.FingerType fingerType) => hand => hand.GetFinger(fingerType);

        public static bool IsPinching(Hand    hand) => hand.IsPinching();
        public static bool IsNotPinching(Hand hand) => !hand.IsPinching();

        public static bool IsGrabbing(Hand    hand) => hand.GrabStrength.IsAlmostEqualTo(1, LeapTolerance);
        public static bool IsNotGrabbing(Hand hand) => !IsGrabbing(hand);

        public static bool IsFist(Hand hand) => hand.GetFistStrength().IsAlmostEqualTo(1, LeapTolerance);

        public static Func<Hand, float> IsMoving() => hand => hand.PalmVelocity.ToVector3().magnitude;

        public static Func<Finger, float> IsPointingTo(Vector4 target) =>
            finger => finger.Direction.ToVector3().normalized.Dot(target.normalized);
    }
}