using Demo.cubeSpawner;
using FluentMotion.detectors;
using Leap;
using UnityEngine;

namespace Demo.Detectors
{
    public class HandIsPinchingDetectorImpl : HandIsPinchingDetector
    {
        public override object[] Parameters => new object[] { GestureType.Pinch };

        public override void OnDetect(Hand input, params object[] others)
        {
            if (!(others[0] is GestureType gestureType)) return;

            CubeSpawner.OnDetect(gestureType);
        }
    }
}