using FluentMotion.finger.impl;
using FluentMotion.hand;
using FluentMotion.helpers;
using Leap.Unity;
using System;
using UniRx;
using UnityEngine;

namespace Assets.Demo
{
    class FingerExtendedDemo : MonoBehaviour
    {
        public GameObject ObjectToSpawn;
        public ReactiveHand Hand;

        public void Start()
        {
            Hand.AsObservable().Sample(TimeSpan.FromSeconds(0.5))
                .When(Thumb.IsExtended, Index.IsExtended)
                .Subscribe(hand => Instantiate(ObjectToSpawn, hand.PalmPosition.ToVector4(), Quaternion.identity));
        }
    }
}
