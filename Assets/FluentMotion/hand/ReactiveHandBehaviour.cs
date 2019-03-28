using Leap;
using Leap.Unity;
using System;
using UniRx;
using UnityEngine;

namespace Assets.FluentMotion.hand
{
    public abstract class ReactiveHandBehaviour : MonoBehaviour
    {
        private Subject<HandModelBase> Subject = new Subject<HandModelBase>();
        public HandModelBase Hand;

        public void Update() => Subject.OnNext(Hand);

        public IObservable<Hand> ReactiveHand => Subject.Select(hand => hand.GetLeapHand());
    }
}
