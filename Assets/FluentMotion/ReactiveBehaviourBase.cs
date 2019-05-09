using System;
using UnityEngine;

namespace FluentMotion
{
    public abstract class ReactiveBehaviourBase<TInput> : MonoBehaviour, IReactiveBehaviour<TInput>
    {
        public int interval;

        public abstract IReactiveComponent<TInput> Callback { get; }
        
        public TimeSpan TimeSpan => TimeSpan.FromSeconds(interval);
    }
}