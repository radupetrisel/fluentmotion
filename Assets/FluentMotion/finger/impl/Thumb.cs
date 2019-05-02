﻿using Leap;
using Leap.Unity;

namespace FluentMotion.finger.impl
{
    public static class Thumb
    {
        public static bool IsExtended(Hand hand) => hand.GetThumb().IsExtended;
        public static bool IsExtended(Finger finger) => finger.IsExtended;
    }
}
