using System;
using FluentMotion.extensions;
using UniRx;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace FluentMotion.detectors
{
    public abstract class SwipeDownDetector : ReactiveHandDetector
    {
        private void Start()
        {
            Hand.IsMovingDown(Player.instance)
                .Subscribe(hand => OnDetect(hand, Parameters));
        }
    }
}