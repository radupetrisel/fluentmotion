using FluentMotion.hand;
using System;
using UniRx;
using UnityEngine;

namespace Assets.Demo
{
    public class PalmSwipeDemo : ReactiveHandDetector
    {
        private GameObject _cube;

        public new void Start()
        {
            base.Start();
            _cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            _cube.transform.Translate(new Vector4(0, 0.5f, 1.0f, 0.0f));
        }

        public override void Detect()
        {
            Hand.Select(hand => hand.PalmVelocity.Magnitude > 0.1 ? new Vector3(0, (float)Math.PI / 18.0f * hand.PalmVelocity.Magnitude) : new Vector3(0, 0))
                .Subscribe(rotation => _cube.transform.Rotate(rotation)).AddTo(HandToTrack);
        }
    }
}
