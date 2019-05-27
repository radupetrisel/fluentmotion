using FluentMotion.extensions;
using UniRx;

namespace FluentMotion.detectors
{
    public abstract class FingerIsExtendedDetector : ReactiveFingerDetector
    {
        public void Start()
        {
            Finger
               .IsExtended()
               .Sample(TimeSpan)
               .ObserveOn(Scheduler.MainThread)
               .Subscribe(finger => OnDetect(finger, Parameters));
        }
    }
}