using System;
using FluentMotion.extensions;
using FluentMotion.finger.impl;
using FluentMotion.hand;
using FluentMotion.helpers;
using Leap;
using Leap.Unity;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

namespace Demo
{
    public class FingerExtendedDemo : MonoBehaviour
    {
        [FormerlySerializedAs("ObjectToSpawn")]
        public GameObject objectToSpawn;
        
        [FormerlySerializedAs("Hand")]
        public ReactiveHand hand;

        public void Start()
        {
            hand.AsObservable()
                .Thumb(It.IsExtended)
                .Index(It.IsExtended)
                .Sample(TimeSpan.FromSeconds(.5))
                .Subscribe(h => Instantiate(objectToSpawn, h.PalmPosition.ToVector4(), Quaternion.identity));
        }
    }
}
