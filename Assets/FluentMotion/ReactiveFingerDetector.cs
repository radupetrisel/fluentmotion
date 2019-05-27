using System;
using FluentMotion.extensions;
using FluentMotion.hand;
using Leap;
using Object = UnityEngine.Object;

namespace FluentMotion
{
    public abstract class ReactiveFingerDetector : ReactiveDetectorBase<Finger>
    {
        public Finger.FingerType fingerType;
        
        protected IObservable<Finger> Finger => gameObject.GetComponent<ReactiveHand>().AsObservable().Finger(fingerType);
    }
}