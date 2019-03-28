using Assets.FluentMotion.finger.impl;
using Assets.FluentMotion.hand;
using Assets.FluentMotion.helpers;
using Assets.FluentMotion.helpers.hand;
using System;
using UniRx;
using UnityEngine;

namespace Assets
{
    class FingerExtendedDemo : ReactiveHandBehaviour
    {
        public GameObject ObjectToSpawn;
        public Camera Camera;

        public void Start()
        {
            ReactiveHand.Sample(TimeSpan.FromSeconds(2))
                .When(Thumb.IsExtended, Index.IsExtended)
                .Subscribe(_ => Instantiate(ObjectToSpawn, Camera.transform.forward + new Vector3(0, 1, 0), Quaternion.identity));
        }
    }
}
