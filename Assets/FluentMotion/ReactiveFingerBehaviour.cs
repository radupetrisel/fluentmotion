using System;
using FluentMotion.extensions;
using FluentMotion.hand;
using Leap;
using UnityEngine;

namespace FluentMotion
{
    public abstract class ReactiveFingerBehaviour : ReactiveBehaviourBase<Finger>
    {
        public Finger.FingerType fingerType;
        public ReactiveFingerComponent callback;

        public override IReactiveComponent<Finger> Callback => callback;

        protected IObservable<Finger> Finger => gameObject.GetComponent<ReactiveHand>().AsObservable().Finger(fingerType);
    }
}