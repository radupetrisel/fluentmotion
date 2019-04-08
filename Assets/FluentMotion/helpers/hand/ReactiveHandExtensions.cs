using FluentMotion.hand;
using Leap;
using Leap.Unity;
using System;
using UniRx;

namespace FluentMotion.helpers.hand
{
    public static class ReactiveHandExtensions
    {
        public static IObservable<Finger> WhenThumb(this ReactiveHandDetector This, Func<Finger, bool> predicate) => This.Hand.Select(hand => hand.GetThumb()).Where(predicate);

    }
}
