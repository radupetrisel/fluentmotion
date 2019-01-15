using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereSpawner : MonoBehaviour {

    public GameObject Sphere;
    public Vector3 Location;

    public void Spawn()
    {
        Instantiate(Sphere, Location, Quaternion.identity);
    }
}
