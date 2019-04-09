using UniRx;
using UnityEngine;

namespace FluentMotion
{
    public abstract class ReactiveDetectorBase<T> : MonoBehaviour, IReactiveDetector<T>
    {
        protected readonly Subject<T> _subject = new Subject<T>();
        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        public CompositeDisposable Disposables => _disposables;

        public abstract void Detect();

        public void OnDestroy()
        {
            Disposables.Dispose();
        }

        public void Start()
        {
            Detect();
        }
    }
}
