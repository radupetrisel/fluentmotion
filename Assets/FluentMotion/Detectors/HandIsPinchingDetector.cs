using FluentMotion.extensions;
using UniRx;

namespace FluentMotion.detectors
{
    public abstract class HandIsPinchingDetector : ReactiveHandDetector
    {
        private void Start()
        {
            Hand.IsPinching()
                .Sample(TimeSpan)
                .ObserveOn(Scheduler.MainThread)
                .Subscribe(hand => OnDetect(hand, Parameters));
        }
    }
}