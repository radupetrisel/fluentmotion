using System;
using FluentMotion;
using FluentMotion.helpers;
using Leap;
using Leap.Unity;
using UniRx;
using UnityEngine;

namespace Assets.FluentMotion.hand
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

            Subject
                .OnNext(Selector(Hands));
        }

        public ReactiveHandsHelper<Hand> Selector(ReactiveHandsHelper<HandModelBase> reactiveObject) =>
            new ReactiveHandsHelper<Hand>
            { LeftHand = reactiveObject.LeftHand.GetLeapHand(), RightHand = reactiveObject.RightHand.GetLeapHand() };

        public Subject<ReactiveHandsHelper<Hand>> Subject => _subject;

        public bool IsValid(ReactiveHandsHelper<HandModelBase> reactiveObject) =>
            reactiveObject.LeftHand.IsTracked && reactiveObject.RightHand.IsTracked;

        /// <inheritdoc />
        public IObservable<ReactiveHandsHelper<Hand>> AsObservable() => Subject;
    }
}
