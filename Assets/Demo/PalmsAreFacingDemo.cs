using System;
using System.Collections;
using FluentMotion;
using FluentMotion.extensions;
using Leap.Unity;
using UniRx;
using UnityEngine;

namespace Demo
{
    public class PalmsAreFacingDemo : ReactiveHandsBehaviour
    {
        public GameObject target;
        public void Start()
        {
            Hands.AreFacing()
                 .Sample(TimeSpan.FromSeconds(.5f))
                 .Subscribe((left, right) => Instantiate(target, (left.PalmPosition + (right.PalmPosition - left.PalmPosition) * .5f).ToVector3(), Quaternion.identity));
        }
    }
}