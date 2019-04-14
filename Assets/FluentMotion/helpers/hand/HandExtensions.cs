using Leap;
using Leap.Unity;

namespace FluentMotion.helpers.hand
{
    public static class HandExtensions
    {
        public static Finger GetFinger(this Hand hand, Finger.FingerType fingerType)
        {
            switch (fingerType)
            {
                case Finger.FingerType.TYPE_THUMB:
                    return hand.GetThumb();
                case Finger.FingerType.TYPE_INDEX:
                    return hand.GetIndex();
                case Finger.FingerType.TYPE_MIDDLE:
                    return hand.GetMiddle();
                case Finger.FingerType.TYPE_RING:
                    return hand.GetRing();
                case Finger.FingerType.TYPE_PINKY:
                    return hand.GetPinky();
                default:
                    return new Finger();
            }
        }
    }
}
