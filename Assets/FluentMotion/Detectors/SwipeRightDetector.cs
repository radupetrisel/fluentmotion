using FluentMotion.extensions;
using UniRx;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace FluentMotion.detectors
{
    public abstract class SwipeRightDetector : ReactiveHandDetector
    {
        private void Start()
        {
            Hand.IsMovingRight(Player.instance)
                .Subscribe(hand => OnDetect(hand, Parameters));
        }
    }
}