using Leap;
using Leap.Unity;

namespace FluentMotion.finger.impl
{
    public static class Ring
    {
        public static bool IsExtended(Hand hand) => hand.GetRing().IsExtended;
        
        public static bool IsNotExtended(Hand hand) => !IsExtended(hand);
    }
}