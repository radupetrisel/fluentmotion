using System;
using FluentMotion;
using FluentMotion.extensions;
using Leap.Unity;
using UniRx;
using UnityEngine;

namespace Demo
{
    public class IsFistDemo : ReactiveHandBehaviour
    {
        public GameObject Object;

        public void Start()
        {
            Hand.IsFist()
                .Sample(TimeSpan.FromSeconds(0.5))
                .Subscribe(hand => Instantiate(Object, hand.PalmPosition.ToVector3(), Quaternion.identity));
        }
    }
}