using Leap;
using Leap.Unity;

namespace FluentMotion.finger.impl
{
    public static class Index
    {
        public static bool IsExtended(Hand hand) => hand.GetIndex().IsExtended;
    }
}
