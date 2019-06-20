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
            Hand.Thumb(It.IsExtended)
                .Index(It.IsExtended)
                .Middle(It.IsNotExtended)
                .Ring(It.IsNotExtended)
                .Pinky(It.IsNotExtended)
                .When(Thumb.IsPointingTo(Vector3.up))
                .Subscribe(hand => OnDetect(hand, GestureType.ThumbIndexExtended));
        }

        public override void OnDetect(Hand input, params object[] others)
        {
            if (!(others[0] is GestureType gestureType)) return;

                CubeSpawner.OnDetect(gestureType);
        }
    }
}