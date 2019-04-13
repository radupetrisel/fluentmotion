using FluentMotion.finger.impl;
using FluentMotion.hand;
using FluentMotion.helpers;
using System;
using Leap.Unity;
using UniRx;
using UnityEngine;

namespace FluentMotion.Demo
{
    class FingerExtendedDemo : ReactiveHandDetector
    {
        public GameObject ObjectToSpawn;

        public override void Detect()
        {
           Hand.Sample(TimeSpan.FromSeconds(0.5))
                .When(Thumb.IsExtended, Index.IsExtended)
                .Subscribe(hand =>
                 {
                     Instantiate(ObjectToSpawn, hand.PalmPosition.ToVector4(), Quaternion.identity);
                     Debug.Log("Extended right");
                 })
                 .AddTo(HandToTrack);
        }
    }
}
