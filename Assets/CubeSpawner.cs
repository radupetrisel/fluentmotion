using Leap;
using Leap.Unity;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    public HandModelBase RightHand;
    public GameObject Cube;

    public void Spawn()
    {
        Vector3 palmPosition = RightHand.GetLeapHand().PalmPosition.ToVector3();
        Vector3 palmDirection = RightHand.GetLeapHand().PalmNormal.ToVector3();

        Instantiate(Cube, palmPosition + .03f * palmDirection, Quaternion.identity);
    }


}
