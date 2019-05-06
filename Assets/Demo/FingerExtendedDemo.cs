using System;
using FluentMotion;
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
    public class FingerExtendedDemo : ReactiveHandBehaviour
    {
        public GameObject objectToSpawn;

        public void Start()
        {
            Hand.Thumb(It.IsExtended)
                .Index(It.IsExtended)
                .Sample(TimeSpan.FromSeconds(.5))
                .Subscribe(h => Instantiate(objectToSpawn, h.PalmPosition.ToVector4(), Quaternion.identity));
        }
    }
}
