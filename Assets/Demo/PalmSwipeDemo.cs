using System;
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
    public class PalmSwipeDemo : MonoBehaviour
    {
        [FormerlySerializedAs("Cube")] public GameObject cubes;

        [FormerlySerializedAs("Hand")] public ReactiveHand hand;

        public void Start()
        {
            hand.AsObservable()
                .IsMovingDown(Player.instance)
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