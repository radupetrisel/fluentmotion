using System;
using FluentMotion.hand;
using FluentMotion.helpers;
using Leap;
using Leap.Unity;
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
            _cube.transform.Translate(Vector3.back * 5);
        }

        public override void Detect()
        {
            Hand.Where(hand => hand.PalmVelocity.AngleTo(Vector.Right) < Math.PI / 6)
                .Where(hand => hand.PalmVelocity.Magnitude > 0.01)
                .Select(hand => Rotation.Right(hand.PalmVelocity.Magnitude.Map(0.01f, 1.0f, 0.1f, 2f)))
                .Subscribe(rotation => _cube.transform.Rotate(rotation.eulerAngles))
                .AddTo(HandToTrack);
        }
    }
}
