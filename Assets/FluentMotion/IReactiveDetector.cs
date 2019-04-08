using UniRx;

namespace FluentMotion
{
    public interface IReactiveDetector<T>
    {
        CompositeDisposable Disposables { get; }
        void Detect();
    }
}
