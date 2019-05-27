using System;
using UnityEngine;
namespace FluentMotion
{
    public abstract class ReactiveDetectorBase<TInput> : MonoBehaviour, IReactiveDetector<TInput>
    {
        public double interval = 3;

        public virtual object[] Parameters => null;

        public abstract void OnDetect(TInput input, params object[] others);
        public TimeSpan TimeSpan => TimeSpan.FromSeconds(interval);
    }
}