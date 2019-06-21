using System;

namespace FluentMotion
{
    public interface IReactiveObject<in TTrackedObject, out TReactiveObject>
    {
        TReactiveObject Selector(TTrackedObject reactiveObject);
        bool IsValid(TTrackedObject reactiveObject);
        IObservable<TReactiveObject> AsObservable();
    }
}