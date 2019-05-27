using FluentMotion.extensions;
using UniRx;

namespace FluentMotion.detectors
{
    public abstract class PalmsAreFacingDetector : ReactiveHandsDetector
    {
        private void Start()
        {
            Hands.PalmsAreFacing()
                 .Sample(TimeSpan)
                 .ObserveOn(Scheduler.MainThread)
                 .Subscribe(hands => OnDetect(hands, Parameters));
            
        }
    }
}