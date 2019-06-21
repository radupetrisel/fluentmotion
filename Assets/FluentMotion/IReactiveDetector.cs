using System;

namespace FluentMotion
{
    public interface IReactiveDetector<in TInput>
    {
        void OnDetect(TInput input, params object[] others);

        TimeSpan TimeSpan { get; }
    }
}