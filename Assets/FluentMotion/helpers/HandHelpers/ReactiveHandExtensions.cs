﻿using Leap;
using Leap.Unity;
using System;
using UniRx;

namespace Assets.FluentMotion.helpers.HandHelpers
{
    public static class ReactiveHandExtensions
    {
        public static IObservable<Finger> WhenThumb(this ReactiveHandBehaviour This, Func<Finger, bool> predicate) => This.ReactiveHand.Select(hand => hand.GetThumb()).Where(predicate);

    }
}
