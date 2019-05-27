using Leap;
using Leap.Unity;

namespace FluentMotion.finger.impl
{
    public static class Pinky
    {
        public static bool IsExtended(Hand hand) => hand.GetPinky().IsExtended;
        public static bool IsNotExtended(Hand hand) => !IsExtended(hand);
    }
}