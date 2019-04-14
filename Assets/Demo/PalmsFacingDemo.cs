using System;
using Assets.FluentMotion.hand;
using UniRx;
using UnityEngine;

namespace Assets.Demo
{
    public sealed class PalmsFacingDemo : MonoBehaviour
    {
        public GameObject GameObject;
        public ReactiveHands Hands;

        public void Start()
        {
            Hands.AsObservable()
                .Sample(TimeSpan.FromSeconds(0.5))
                .Where(hands => hands.LeftHand.PalmNormal.Dot(hands.RightHand.PalmNormal) < -.5f)
                .Subscribe(hands => Instantiate(GameObject, Camera.current.transform.position + Camera.current.transform.forward.normalized * 2, Quaternion.identity));
        }
    }
}
