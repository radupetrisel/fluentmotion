using Leap;
using Leap.Unity;
using System;
using UniRx;
using UnityEngine;

namespace FluentMotion.hand
{
    public abstract class ReactiveFingerDetector : ReactiveDetectorBase<Finger>
    {
        public FingerModel FingerToTrack;

        public void Update() => _subject.OnNext(FingerToTrack.GetLeapFinger());

        public IObservable<Finger> Finger => _subject;
    }
}
