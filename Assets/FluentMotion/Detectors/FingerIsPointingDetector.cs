using FluentMotion.extensions;
using UniRx;
using UnityEngine;

namespace FluentMotion.detectors
{
    public abstract class FingerIsPointingDetector : ReactiveFingerDetector
    {
        public GameObject target;

        private void Start()
        {
            Finger
               .IsPointingTo(target, t => t.transform.position)
               .Sample(TimeSpan)
               .ObserveOn(Scheduler.MainThread)
               .Subscribe(finger => OnDetect(finger, Parameters));
        }
    }
}