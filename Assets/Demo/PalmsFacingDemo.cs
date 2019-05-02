using System;
using FluentMotion.hand;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

namespace Demo
{
    public sealed class PalmsFacingDemo : MonoBehaviour
    {
        [FormerlySerializedAs("ObjectToSpan")]
        public GameObject objectToSpan;
        
        [FormerlySerializedAs("Hands")]
        public ReactiveHands hands;

        public void Start()
        {
            hands.AsObservable()
                .Sample(TimeSpan.FromSeconds(0.5))
                .Where(h => h.LeftHand.PalmNormal.Dot(h.RightHand.PalmNormal) < -.5f)
                .Subscribe(h => Instantiate(objectToSpan, Camera.current.transform.position + Camera.current.transform.forward.normalized * 2, Quaternion.identity));
        }
    }
}
