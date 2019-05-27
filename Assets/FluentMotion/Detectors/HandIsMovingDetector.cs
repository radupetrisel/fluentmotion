using FluentMotion.extensions;
using UniRx;
using Valve.VR.InteractionSystem;

namespace FluentMotion.detectors
{
    public abstract class HandIsMovingDetector : ReactiveHandDetector
    {
        public SwipeDirection direction;
        
        private void Start()
        {
            Hand.IsMoving(Player.instance,
                     direction)
                .Sample(TimeSpan)
                .ObserveOn(Scheduler.MainThread)
                .Subscribe(hand => OnDetect(hand, Parameters));
        }
    }
}