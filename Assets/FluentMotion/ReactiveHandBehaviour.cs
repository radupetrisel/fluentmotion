using System;
using FluentMotion.hand;
using Leap;
using UnityEngine;

namespace FluentMotion
{
    public abstract class ReactiveHandBehaviour : ReactiveBehaviourBase<Hand>
    {
        protected IObservable<Hand> Hand => gameObject.GetComponent<ReactiveHand>().AsObservable();
        
        public ReactiveHandComponent callback;

        public override IReactiveComponent<Hand> Callback => callback;
    }
}