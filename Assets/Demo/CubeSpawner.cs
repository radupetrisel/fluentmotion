using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Demo.cubeSpawner
{
    public class CubeSpawner : MonoBehaviour
    {
        private List<Expectation> _possibleGestures;
        private const string MaterialsDir = "Images/Materials";

        public static CubeSpawner Instance { get; private set; }

        public Subject<Unit> Subject { get; private set; }

        public Subject<GestureType> Gesture { get; private set; }

        private Spawnee Spawnee { get; set; }

        public void Start()
        {
            Subject = new Subject<Unit>();
            Gesture = new Subject<GestureType>();

            if (Instance == null)
            {
                Instance = FindObjectOfType<CubeSpawner>();
            }

            _possibleGestures = Resources.LoadAll<Material>(MaterialsDir)
                                         .Select(material => new Expectation(material))
                                         .ToList();
            
            Subject.AsObservable()
                   .StartWith(Unit.Default)
                   .Subscribe(_ => Spawn());

            Gesture.AsObservable()
                   .Sample(TimeSpan.FromSeconds(0.5))
                   .ObserveOn(Scheduler.MainThread)
                   .Subscribe(gestureType =>
                              {
                                  if (Instance.Spawnee.Expectation.Gesture != gestureType) return;
                                  
                                  Spawnee.Destroy();
                              });
        }

        private void Spawn()
        {
            var spawneeObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
            spawneeObject.AddComponent<Spawnee>();
            var spawnee = spawneeObject.GetComponent<Spawnee>();
            spawnee.CubeSpawner = this;
            spawnee.Expectation = GetRandomExpectation();
            
            var meshRenderer = spawneeObject.GetComponent<MeshRenderer>();
            meshRenderer.material = spawnee.Expectation.Material;

            Spawnee = spawnee.GetComponent<Spawnee>();
            Debug.Log($"Spawnee is expecting {Spawnee.Expectation.Gesture}");
        }

        private Expectation GetRandomExpectation() => _possibleGestures[(int) (Random.value * _possibleGestures.Count)];
        

        public static void OnDetect(GestureType gestureType)
        {
            Instance.Gesture.OnNext(gestureType);
        }
    }
}