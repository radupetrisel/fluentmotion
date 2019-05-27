using System;
using FluentMotion.hand;
using FluentMotion.helpers;
using Leap;

namespace FluentMotion
{
    public abstract class ReactiveHandsDetector : ReactiveDetectorBase<ReactiveHandsHelper<Hand>>
    {
        protected IObservable<ReactiveHandsHelper<Hand>> Hands => gameObject.GetComponent<ReactiveHands>().AsObservable();
    }
}