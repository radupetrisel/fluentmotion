using Assets.FluentMotion.hand;
using UniRx;
using UnityEngine;

namespace Assets.Demo
{
    public sealed class PalmsFacingDemo : ReactiveHandsDetector
    {
        public GameObject GameObject;

        public override void Detect()
        {
            Hands
                .Where(hands => hands.LeftHand.PalmNormal.Dot(hands.RightHand.PalmNormal) < -.5f)
                .Subscribe(hands => Instantiate(GameObject, Camera.current.transform.position + Camera.current.transform.forward.normalized * 2, Quaternion.identity));
        }
    }
}
