using FluentMotion.finger.impl;
using FluentMotion.hand;
using FluentMotion.helpers;
using System;
using UniRx;
using UnityEngine;

namespace Assets.Demo
{
    class PalmSwipeDemo : ReactiveHandDetector
    {
        private GameObject Cube;

        public new void Start()
        {
            base.Start();
            Cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Cube.transform.Translate(new Vector4(0, 0.5f, 1.0f, 0.0f));
        }

        public override void Detect()
        {
            Hand.Select(hand => hand.PalmVelocity.Magnitude > 0.1 ? new Vector3(0, (float)Math.PI / 18.0f * hand.PalmVelocity.Magnitude) : new Vector3(0, 0))
                .Subscribe(rotation => Cube.transform.Rotate(rotation)).AddTo(HandToTrack);
        }
    }
}
