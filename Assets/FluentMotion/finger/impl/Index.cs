using Leap;
using Leap.Unity;

namespace Assets.FluentMotion.finger.impl
{
    class Index : AbstractReactiveFinger
    {
        public static bool IsExtended(Hand hand) => hand.GetIndex().IsExtended;
    }
}
