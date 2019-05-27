using System;
using FluentMotion.hand;
using Leap;
using UnityEngine;

namespace FluentMotion
{
    public abstract class ReactiveHandDetector : ReactiveDetectorBase<Hand>
    {
        protected IObservable<Hand> Hand => gameObject.GetComponent<ReactiveHand>().AsObservable();
    }
}