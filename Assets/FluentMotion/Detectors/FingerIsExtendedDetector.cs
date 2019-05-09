using UniRx;

namespace FluentMotion.detectors
{
    public class FingerIsExtendedDetector : ReactiveFingerBehaviour
    {
        private void Start()
        { 
            Finger           
                .Sample(TimeSpan)
                .Subscribe(Callback.OnDetect);
        }
    }
}