using System;
using UniRx;
using UnityEngine;

namespace Demo.cubeSpawner
{
    [Serializable]
    public class Spawnee : MonoBehaviour
    {
        public CubeSpawner CubeSpawner { private get; set; }
        
        public Expectation Expectation { get; set; }

        public void Start()
        {
            transform.position = new Vector3(0, 0, - 5);
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        public void OnDestroy()
        {
            CubeSpawner.Subject.OnNext(Unit.Default);
        }
    }
}