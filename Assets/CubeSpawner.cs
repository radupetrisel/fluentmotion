using Leap;
using Leap.Unity;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{

    public CapsuleHand LeftHand;
    public CapsuleHand RightHand;
    public GameObject Cube;

    public void Spawn()
    {
        Instantiate(Cube, RightHand.GetLeapHand().GetThumb().TipPosition.ToVector3(), Quaternion.identity);
    }


}
