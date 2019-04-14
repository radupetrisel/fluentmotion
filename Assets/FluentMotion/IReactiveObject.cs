using System;
using UniRx;

namespace FluentMotion
{
    public interface IReactiveObject<in T, U>
    {
        U Selector(T reactiveObject);
        Subject<U> Subject { get; }
        bool IsValid(T reactiveObject);

        IObservable<U> AsObservable();
    }
}
