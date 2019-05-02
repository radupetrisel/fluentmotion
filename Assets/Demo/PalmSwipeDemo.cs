using System;
using FluentMotion.hand;
using FluentMotion.helpers;
using Leap;
using Leap.Unity;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

namespace Demo
{
    public class PalmSwipeDemo : MonoBehaviour
    {
        private GameObject _cube;
        
        [FormerlySerializedAs("Hand")] 
        public ReactiveHand hand;

        public void Start()
        {
            _cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            _cube.transform.Translate(Vector3.back * 5);

            hand.AsObservable()
                .Where(h => h.PalmVelocity.AngleTo(Vector.Right) < Math.PI / 6)
                .Where(h => h.PalmVelocity.Magnitude > 0.01)
                .Select(h => Rotation.Right(h.PalmVelocity.Magnitude.Map(0.01f, 1.0f, 0.1f, 2f)))
                .Subscribe(rotation => _cube.transform.Rotate(rotation.eulerAngles));
        }
    }
}
