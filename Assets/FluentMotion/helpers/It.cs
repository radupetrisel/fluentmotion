using System;
using Leap;
using Leap.Unity;

namespace FluentMotion.helpers
{
    public static class It
    {
        public static bool IsExtended(Finger finger) => finger.IsExtended;

        public static Finger GetThumb(Hand hand) => hand.GetThumb();
        public static Finger GetIndex(Hand hand) => hand.GetIndex();
        public static Finger GetMiddle(Hand hand) => hand.GetMiddle();
        public static Finger GetRing(Hand hand) => hand.GetRing();
        public static Finger GetPinky(Hand hand) => hand.GetPinky();

        public static bool IsPinching(Hand hand) => hand.IsPinching();
        public static bool IsNotPinching(Hand hand) => !hand.IsPinching();
        
        public static bool IsGrabbing(Hand hand) => Math.Abs(hand.GrabStrength - 1) < float.Epsilon;
        public static bool IsNotGrabbing(Hand hand) => !IsGrabbing(hand);
        
        
    }
}