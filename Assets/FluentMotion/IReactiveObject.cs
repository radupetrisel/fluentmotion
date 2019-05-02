using System;
using UniRx;

namespace FluentMotion
{
    public interface IReactiveObject<in T, out U>
    {
        U Selector(T reactiveObject);
        bool IsValid(T reactiveObject);
        IObservable<U> AsObservable();
    }
}
