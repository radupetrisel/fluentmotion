using System;

namespace FluentMotion
{
    public interface IReactiveBehaviour<in TInput>
    {
        IReactiveComponent<TInput> Callback{ get; }

        TimeSpan TimeSpan { get; }
    }
}