using System.Collections.Generic;
using System.Linq;
using Leap;

namespace FluentMotion.helpers
{
    public static class All
    {
        public static bool AreExtended(IEnumerable<Finger> fingers) => fingers.All(finger => finger.IsExtended);
    }
}