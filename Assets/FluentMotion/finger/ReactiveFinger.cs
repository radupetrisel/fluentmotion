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

            _subject.OnNext(Selector(Hand));
        }

        public Finger Selector(HandModelBase reactiveObject) => reactiveObject.GetLeapHand().GetFinger(FingerType);
        
        public bool IsValid(HandModelBase reactiveObject) => reactiveObject.IsTracked;

        public IObservable<Finger> AsObservable() => _subject;
    }
}
