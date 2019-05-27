using FluentMotion.extensions;
using Leap;
using UniRx;
using UnityEngine;

namespace FluentMotion.detectors
{
    public abstract class PalmIsFacingDetector : ReactiveHandDetector
    {
        public GameObject target;
        
        private void Start()
        {
            Hand.PalmIsFacing(target, t => t.transform.position)
                .Sample(TimeSpan)
                .ObserveOn(Scheduler.MainThread)
                .Subscribe(hand => OnDetect(hand, Parameters));
        }
    }
}