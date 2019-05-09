using FluentMotion.extensions;
using UniRx;
using UnityEngine;

namespace FluentMotion.detectors
{
    public class PalmIsFacingDetector : ReactiveHandBehaviour
    {
        public GameObject target;
        
        private void Start()
        {
            Hand.PalmIsFacing(target, t => t.transform.position)
                .Sample(TimeSpan)
                .Subscribe(Callback.OnDetect);
        }
    }
}