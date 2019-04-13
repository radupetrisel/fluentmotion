using FluentMotion;
using FluentMotion.helpers;
using Leap.Unity;
using System;
using UniRx;

namespace Assets.FluentMotion.hand
{
    public abstract class ReactiveHandsDetector : ReactiveDetectorBase<ReactiveHands>
    {
        public HandModelBase TrackedHandLeft;
        public HandModelBase TrackedHandRight;

        public void Update()
        {
            if (!TrackedHandLeft.IsTracked || !TrackedHandRight.IsTracked) return;

            _subject
                .OnNext(new ReactiveHands
                {
                    LeftHand = TrackedHandLeft.GetLeapHand(),
                    RightHand = TrackedHandRight.GetLeapHand()
                });
        }

        public IObservable<ReactiveHands> Hands => _subject;
    }
}
