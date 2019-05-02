using FluentMotion.helpers;
using Leap;
using Leap.Unity;
using System;
using UniRx;
using UnityEngine;

namespace FluentMotion.hand
{
    public sealed class ReactiveHands : MonoBehaviour, IReactiveObject<ReactiveHandsHelper<HandModelBase>, ReactiveHandsHelper<Hand>>
    {
        public HandModelBase TrackedHandLeft;
        public HandModelBase TrackedHandRight;
        private ReactiveHandsHelper<HandModelBase> Hands => new ReactiveHandsHelper<HandModelBase> { LeftHand = TrackedHandLeft, RightHand = TrackedHandRight };
        private readonly Subject<ReactiveHandsHelper<Hand>> _subject = new Subject<ReactiveHandsHelper<Hand>>();

        public void Update()
        {
            if (!IsValid(Hands)) return;

            _subject
                .OnNext(Selector(Hands));
        }

        public ReactiveHandsHelper<Hand> Selector(ReactiveHandsHelper<HandModelBase> reactiveObject) =>
            new ReactiveHandsHelper<Hand>
            { LeftHand = reactiveObject.LeftHand.GetLeapHand(), RightHand = reactiveObject.RightHand.GetLeapHand() };

        public bool IsValid(ReactiveHandsHelper<HandModelBase> reactiveObject) =>
            reactiveObject.LeftHand.IsTracked && reactiveObject.RightHand.IsTracked;

        public IObservable<ReactiveHandsHelper<Hand>> AsObservable() => _subject;
    }
}
