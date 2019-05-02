using System;
using FluentMotion.helpers.hand;
using Leap;
using Leap.Unity;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

namespace FluentMotion.hand
{
    public sealed class ReactiveHand : MonoBehaviour, IReactiveObject<HandModelBase, Hand>
    {
        [FormerlySerializedAs("Hand")]
        public HandModelBase hand;
        
        private readonly Subject<Hand> _subject = new Subject<Hand>();

        public void OnDestroy()
        {
            _subject.Dispose();
        }

        public void Update()
        {
            if (!IsValid(hand)) return;

            _subject.OnNext(Selector(hand));
        }

        public Hand Selector(HandModelBase reactiveObject) => reactiveObject.GetLeapHand();
        public bool IsValid(HandModelBase reactiveObject) => reactiveObject.IsTracked;
        public IObservable<Hand> AsObservable() => _subject;

        public IObservable<Finger> Thumb => _subject.Select(h => h.GetThumb());
        public IObservable<Finger> Index => _subject.Select(h => h.GetIndex());
        public IObservable<Finger> Middle => _subject.Select(h => h.GetMiddle());
        public IObservable<Finger> Ring => _subject.Select(h => h.GetRing());
        public IObservable<Finger> Pinky => _subject.Select(h => h.GetPinky());

        public IObservable<Hand> IsPinching => _subject.Where(h => h.IsPinching());
    }
}
