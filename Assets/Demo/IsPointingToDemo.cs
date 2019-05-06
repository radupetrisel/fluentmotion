using System;
using FluentMotion;
using FluentMotion.extensions;
using Leap.Unity;
using UniRx;
using UnityEngine;

namespace Demo
{
    public sealed class IsPointingToDemo : ReactiveHandBehaviour
    {
        public GameObject target;
        public GameObject objectToInstantiate;

        public void Start()
        {
            Hand.Thumb()
                .IsPointingTo(target, t => t.transform.position)
                .Sample(TimeSpan.FromSeconds(0.5))
                .Subscribe(finger => Instantiate(objectToInstantiate, finger.TipPosition.ToVector3(), Quaternion.identity));
        }
    }
}