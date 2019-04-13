using UniRx;

namespace FluentMotion
{
    public interface IReactiveDetector
    {
        CompositeDisposable Disposables { get; }
        void Detect();
    }
}
