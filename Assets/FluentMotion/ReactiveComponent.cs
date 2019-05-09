using FluentMotion.helpers;
using Leap;
using UnityEngine;

namespace FluentMotion
{

    public interface IReactiveComponent<in TInput>
    {
        void OnDetect(TInput input);
    }

    public abstract class ReactiveFingerComponent : MonoBehaviour, IReactiveComponent<Finger>
    {
        public abstract void OnDetect(Finger input);
    }

    public abstract class ReactiveHandComponent : MonoBehaviour, IReactiveComponent<Hand>
    {
        public abstract void OnDetect(Hand input);
    }

    public abstract class ReactiveHandsComponent : MonoBehaviour, IReactiveComponent<ReactiveHandsHelper<Hand>>
    {
        public abstract void OnDetect(ReactiveHandsHelper<Hand> input);
    }
    
}