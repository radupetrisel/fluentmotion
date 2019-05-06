using Leap;
using Leap.Unity;

namespace FluentMotion.finger.impl
{
    public class Pinky
    {
        public static bool IsExtended(Hand hand) => hand.GetPinky().IsExtended;
    }
}