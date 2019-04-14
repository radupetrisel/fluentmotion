using System;
using Leap;
using Leap.Unity;
using UniRx;
using UnityEngine;

namespace FluentMotion.hand
{
    public sealed class ReactiveHand : MonoBehaviour, IReactiveObject<HandModelBase, Hand>
    {
        public HandModelBase Hand;
        private readonly Subject<Hand> _subject = new Subject<Hand>();

        public void OnDestroy()
        {
            Subject.Dispose();
        }

        public void Update()
        {
            if (!IsValid(Hand)) return;

            _subject.OnNext(Selector(Hand));
        }

        public Hand Selector(HandModelBase reactiveObject) => reactiveObject.GetLeapHand();
        public Subject<Hand> Subject => _subject;
        public bool IsValid(HandModelBase reactiveObject) => reactiveObject.IsTracked;
        public IObservable<Hand> AsObservable() => Subject;
    }
}
