using Leap;
using Leap.Unity;
using System;
using UniRx;

namespace FluentMotion.helpers.hand
{
    public static class ReactiveHandExtensions
    {
        public static IObservable<Finger> WhenThumb(this IObservable<Hand> This, Func<Finger, bool> predicate) =>
            This.When(hand => predicate(hand.GetThumb())).Select(hand => hand.GetThumb());
    }
}
