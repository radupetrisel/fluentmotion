using System;
using FluentMotion;
using FluentMotion.extensions;
using UniRx;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Demo
{
    public class IsPinchingDemo : ReactiveHandBehaviour
    {
        public GameObject target;

        public void Start()
        {
            Hand.IsPinching()
                .Subscribe(hand => Instantiate(target, Player.instance.hmdTransform.position + Player.instance.hmdTransform.forward.normalized * 2,
                                               Quaternion.identity));
        }
    }
}