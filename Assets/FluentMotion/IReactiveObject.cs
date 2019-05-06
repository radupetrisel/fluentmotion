using System;
using UniRx;

namespace FluentMotion
{
    public interface IReactiveObject { }

    public interface IReactiveObject<in TTrackedObject, out TReactiveObject> : IReactiveObject
    {
        TReactiveObject              Selector(TTrackedObject reactiveObject);
        bool                         IsValid(TTrackedObject  reactiveObject);
        IObservable<TReactiveObject> AsObservable();
    }
}