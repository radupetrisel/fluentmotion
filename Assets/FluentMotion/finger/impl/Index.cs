using Leap;
using Leap.Unity;

namespace FluentMotion.finger.impl
{
    class Index : AbstractReactiveFinger
    {
        public static bool IsExtended(Hand hand) => hand.GetIndex().IsExtended;
    }
}
