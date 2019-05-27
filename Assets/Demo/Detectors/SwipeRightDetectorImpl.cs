using Demo.cubeSpawner;
using FluentMotion.detectors;
using Leap;

namespace Demo.Detectors
{
    public class SwipeRightDetectorImpl : SwipeRightDetector
    {
        public override object[] Parameters => new object[] { GestureType.SwipeRight };

        public override void OnDetect(Hand input, params object[] others)
        {
            if (!(others[0] is GestureType gestureType)) return;

            CubeSpawner.OnDetect(gestureType);
        }
    }
}