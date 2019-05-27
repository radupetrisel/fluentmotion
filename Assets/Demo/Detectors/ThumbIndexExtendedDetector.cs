using Demo.cubeSpawner;
using FluentMotion;
using FluentMotion.extensions;
using FluentMotion.finger.impl;
using FluentMotion.helpers;
using Leap;
using UniRx;
using UnityEngine;

namespace Demo.Detectors
{
    public class ThumbIndexExtendedDetector : ReactiveHandDetector
    {
        private void Start()
        {
            Hand.When(Thumb.IsExtended,
                     Index.IsExtended,
                     Middle.IsNotExtended,
                     Ring.IsNotExtended,
                     Pinky.IsNotExtended
                     )
                .Subscribe(hand => OnDetect(hand));
        }

        public override void OnDetect(Hand input, params object[] others)
        {
            if (!(others[0] is GestureType gestureType)) return;

                CubeSpawner.OnDetect(gestureType);
        }
    }
}