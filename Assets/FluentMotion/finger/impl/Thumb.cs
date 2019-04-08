using Leap;
using Leap.Unity;

namespace FluentMotion.finger.impl
{
    public class Thumb : AbstractReactiveFinger
    {
        public static bool IsExtended(Hand hand) => hand.GetThumb().IsExtended;
    }
}
