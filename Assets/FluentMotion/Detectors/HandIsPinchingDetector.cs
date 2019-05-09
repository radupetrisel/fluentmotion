using FluentMotion.extensions;
using UniRx;

namespace FluentMotion.detectors
{
    public class HandIsPinchingDetector : ReactiveHandBehaviour
    {
        private void Start()
        {
            Hand.IsPinching()
                .Sample(TimeSpan)
                .Subscribe(Callback.OnDetect);
        }
    }
}