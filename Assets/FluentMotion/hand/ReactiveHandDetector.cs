using Leap;
using Leap.Unity;
using System;
using UniRx;
using UnityEngine;

namespace FluentMotion.hand
{
    public abstract class ReactiveHandDetector : ReactiveDetectorBase<Hand>
    {
        public HandModelBase HandToTrack;

        public void Update()
        {
            if (HandToTrack.GetLeapHand() is null) return;

            if (!HandToTrack.IsTracked) return;
            
            _subject.OnNext(HandToTrack.GetLeapHand());
        }

        public IObservable<Hand> Hand => _subject;
    }
}
