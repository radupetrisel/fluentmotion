using Demo.cubeSpawner;
using FluentMotion.detectors;
using Leap;
using UnityEngine;

namespace Demo.Detectors
{
    public class HandIsFistDetectorImpl : HandIsFistDetector
    {
        public override object[] Parameters => new object[] {GestureType.Fist};

        public override void OnDetect(Hand input, params object[] others)
        {
            if (!(others[0] is GestureType gestureType)) return;

            CubeSpawner.OnDetect(gestureType);
        }
    }
}