using FluentMotion.extensions;
using UniRx;
using UnityEngine;

namespace FluentMotion.detectors
{
    public class FingerIsPointingDetector : ReactiveFingerBehaviour
    {
        public GameObject target;

        private void Start()
        {
            Finger
               .IsPointingTo(target, t => t.transform.position)
               .Sample(TimeSpan)
               .Subscribe(Callback.OnDetect);
        }
    }
}