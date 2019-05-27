using System;
using FluentMotion.extensions;
using UniRx;
using Valve.VR.InteractionSystem;

namespace FluentMotion.detectors
{
    public abstract class SwipeUpDetector : ReactiveHandDetector
    {
        private void Start()
        {
            Hand.IsMovingUp(Player.instance)
                .Subscribe(hand => OnDetect(hand, Parameters));
        }
    }
}