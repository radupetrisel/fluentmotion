using FluentMotion.helpers.hand;
using Leap;
using Leap.Unity;
using System;
using UniRx;
using UnityEngine;

namespace FluentMotion.finger
{
    public sealed class ReactiveFinger : MonoBehaviour, IReactiveObject<HandModelBase, Finger>
    {
        private readonly Subject<Finger> _subject = new Subject<Finger>();
        public Finger.FingerType FingerType;
        public HandModelBase Hand;

        public void Update()
        {
            if (!IsValid(Hand)) return;

            Subject.OnNext(Selector(Hand));
        }

        public Finger Selector(HandModelBase reactiveObject) => reactiveObject.GetLeapHand().GetFinger(FingerType);
        public Subject<Finger> Subject => _subject;

        /// <inheritdoc />
        public bool IsValid(HandModelBase reactiveObject) => reactiveObject.IsTracked;

        /// <inheritdoc />
        public IObservable<Finger> AsObservable() => Subject;
    }
}
