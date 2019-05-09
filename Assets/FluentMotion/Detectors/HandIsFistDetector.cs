using FluentMotion.extensions;
using UniRx;

namespace FluentMotion.detectors
{
    public class HandIsFistDetector : ReactiveHandBehaviour
    {
        private void Start()
        {
            Hand.IsFist()
                .Sample(TimeSpan)
                .Subscribe(Callback.OnDetect);
        }
    }
}