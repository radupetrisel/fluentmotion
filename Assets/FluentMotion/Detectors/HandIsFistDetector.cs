using FluentMotion.extensions;
using UniRx;

namespace FluentMotion.detectors
{
    public abstract class HandIsFistDetector : ReactiveHandDetector
    {
        private void Start()
        {
            Hand.IsFist()
                .Sample(TimeSpan)
                .ObserveOn(Scheduler.MainThread)
                .Subscribe(hand => OnDetect(hand, Parameters));
        }
    }
}