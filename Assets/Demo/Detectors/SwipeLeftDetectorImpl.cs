using Demo.cubeSpawner;
using FluentMotion.detectors;
using Leap;

namespace Demo.Detectors
{
    public class SwipeLeftDetectorImpl : SwipeLeftDetector
    {
        public override object[] Parameters => new object[] { GestureType.SwipeLeft };

        public override void OnDetect(Hand input, params object[] others)
        {
            if (!(others[0] is GestureType gestureType)) return;

            CubeSpawner.OnDetect(gestureType);
        }
    }
}