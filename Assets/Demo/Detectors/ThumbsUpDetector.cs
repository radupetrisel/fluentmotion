using System.Numerics;
using Demo.cubeSpawner;
using FluentMotion;
using FluentMotion.extensions;
using FluentMotion.helpers;
using UniRx;
using Valve.VR.InteractionSystem;
using Hand = Leap.Hand;

namespace Demo.Detectors
{
    public class ThumbsUpDetector : ReactiveHandDetector
    {
        private void Start()
        {
            Hand.Thumb(It.IsExtended)
                .Index(It.IsNotExtended)
                .Subscribe(_ => OnDetect(_, GestureType.ThumbsUp));
        }

        public override void OnDetect(Hand _, params object[] others)
        {
            if (!(others[0] is GestureType gestureType)) return;

                CubeSpawner.OnDetect(gestureType);
        }
    }
}