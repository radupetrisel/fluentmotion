using FluentMotion.extensions;
using UniRx;
using Valve.VR.InteractionSystem;

namespace FluentMotion.detectors
{
    public class PalmSwipeDetector : ReactiveHandBehaviour
    {
        public SwipeDirection direction;

        private void Start()
        {
            Hand.IsMoving(Player.instance, direction)
                .Sample(TimeSpan)
                .Subscribe(Callback.OnDetect);
        }
    }
}