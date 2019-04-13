using UnityEngine;

namespace FluentMotion.helpers
{
    public static class Rotation
    {
        public static Quaternion Right(float degrees) => Quaternion.AngleAxis(degrees, Vector3.up);
        public static Quaternion Left(float degrees) => Quaternion.AngleAxis(degrees, Vector3.down);
        public  static Quaternion Forward(float degrees) => Quaternion.AngleAxis(degrees, Vector3.right);
        public static Quaternion Backward(float degrees) => Quaternion.AngleAxis(degrees, Vector3.left);
        public static Quaternion Up(float degrees) => Quaternion.AngleAxis(degrees, Vector3.back);
        public static Quaternion Down(float degrees) => Quaternion.AngleAxis(degrees, Vector3.forward);
    }
}
