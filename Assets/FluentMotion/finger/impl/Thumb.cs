using Assets.FluentMotion.hand;
using Leap;
using Leap.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.FluentMotion.finger.impl
{
    public class Thumb : AbstractReactiveFinger
    {
        public static bool IsExtended(Hand hand) => hand.GetThumb().IsExtended;
    }
}
