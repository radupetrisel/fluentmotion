using System;
using FluentMotion.extensions;
using UniRx;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace FluentMotion.detectors
{
    public abstract class SwipeLeftDetector : ReactiveHandDetector
    {
        private void Start()
        {
            Hand.IsMovingLeft(Player.instance)
                .Subscribe(hand => OnDetect(hand, Parameters));
        }
    }
}