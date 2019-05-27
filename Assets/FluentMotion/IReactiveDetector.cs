using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace FluentMotion
{
    public interface IReactiveDetector<in TInput>
    {
        void OnDetect(TInput input, params object[] others);

        TimeSpan TimeSpan { get; }
    }
}