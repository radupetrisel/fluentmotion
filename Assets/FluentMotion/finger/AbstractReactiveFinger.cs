using Leap;
using Leap.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.FluentMotion.finger
{
    public abstract class AbstractReactiveFinger
    {
        public static bool IsExtended(Finger finger) => finger.IsExtended;

    }
}
