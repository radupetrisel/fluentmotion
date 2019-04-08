using FluentMotion.helpers;
using FluentMotion.finger.impl;
using FluentMotion.hand;
using System;
using UniRx;
using UnityEngine;

namespace FluentMotion.Demo
{
    class FingerExtendedDemo : ReactiveHandDetector
    {
        public GameObject ObjectToSpawn;
        public Camera Camera;

        public override void Detect()
        {
           Hand.Sample(TimeSpan.FromSeconds(2))
                .When(Thumb.IsExtended, Index.IsExtended)
                .Subscribe(_ => Instantiate(ObjectToSpawn, Camera.transform.forward + new Vector3(0, 1, 0), Quaternion.identity))
                .DisposedBy(Disposables);
        }
    }
}
