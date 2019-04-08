using Leap;
using Leap.Unity;
using System;

namespace FluentMotion.hand
{
    public abstract class ReactiveHandDetector : ReactiveDetectorBase<Hand>
    {
        public HandModelBase HandToTrack;

        public void Update() => _subject.OnNext(HandToTrack.GetLeapHand());

        public IObservable<Hand> Hand => _subject;
    }
}
