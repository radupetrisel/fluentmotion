using System;
using System.Linq;
using UniRx;

namespace FluentMotion.helpers
{
    public static class ObservableExtensions
    {
        public static IObservable<T> When<T>(this IObservable<T> observable, params Func<T, bool>[] actions) => observable.Where(elem => actions.Select(f => f(elem)).Aggregate((f1, f2) => f1 && f2));

    }
}
