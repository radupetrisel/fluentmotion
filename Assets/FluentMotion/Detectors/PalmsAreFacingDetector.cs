using FluentMotion.extensions;
using UniRx;

namespace FluentMotion.detectors
{
    public class PalmsAreFacingDetector : ReactiveHandsBehaviour
    {
        private void Start()
        {
            Hands.PalmsAreFacing()
                 .Sample(TimeSpan)
                 .Subscribe(Callback.OnDetect);
        }
    }
}