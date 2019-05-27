using Demo.cubeSpawner;
using FluentMotion.detectors;
using Leap;
using UnityEngine;

namespace Demo.Detectors
{
    public class SwipeUpDetectorImpl : SwipeUpDetector
    {
        public override object[] Parameters => new object[] { GestureType.SwipeUp };

        public override void OnDetect(Hand input, params object[] others)
        {
            if (!(others[0] is GestureType gestureType)) return;
            
            CubeSpawner.OnDetect(gestureType);
        }
    }
}