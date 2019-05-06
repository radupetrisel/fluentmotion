using Leap;
using Leap.Unity;

namespace FluentMotion.finger.impl
{
    public class Ring
    {
        public static bool IsExtended(Hand hand) => hand.GetRing().IsExtended;
    }
}