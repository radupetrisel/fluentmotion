using System;
using FluentMotion.hand;
using FluentMotion.helpers;
using Leap;
using UnityEngine;

namespace FluentMotion
{
    public abstract class ReactiveHandsBehaviour : ReactiveBehaviourBase<ReactiveHandsHelper<Hand>>
    {
        protected IObservable<ReactiveHandsHelper<Hand>> Hands => gameObject.GetComponent<ReactiveHands>().AsObservable();
        
        public ReactiveHandsComponent callback;

        public override IReactiveComponent<ReactiveHandsHelper<Hand>> Callback => callback;
    }
}