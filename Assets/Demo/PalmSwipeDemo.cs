using System;
using FluentMotion;
using FluentMotion.extensions;
using FluentMotion.hand;
using FluentMotion.helpers;
using Leap;
using Leap.Unity;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;
using Valve.VR.InteractionSystem;

namespace Demo
{
    public class PalmSwipeDemo : ReactiveHandBehaviour
    {
        public GameObject cubes;

        public void Start()
        {
            Hand.IsMovingDown(Player.instance)
                .Select(h => Rotation.Right(h.PalmVelocity.ToVector3().magnitude.Map(0.1f, 2, 0.1f, 2f)))
                .Subscribe(rotation =>
                           {
                               foreach (Transform child in cubes.transform)
                               {
                                   child.Rotate(rotation.eulerAngles);
                               }
                           });
        }
    }
}