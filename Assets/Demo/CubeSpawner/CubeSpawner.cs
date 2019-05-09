using System;
using UniRx;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Demo.CubeSpawner
{
    public class CubeSpawner : MonoBehaviour
    {
        private GameObject _cube;
        private Subject<Unit> _subject;

        public void Start()
        {
            _subject = new Subject<Unit>();

            _cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            _cube.AddComponent<Rigidbody>();
            var body = _cube.GetComponent<Rigidbody>();
            body.AddForce(new Vector3(0, 0, 10));

            _subject.AsObservable()
                    .Sample(TimeSpan.FromSeconds(1))
                    .Subscribe(_ => Instantiate(_cube,
                                   Player.instance.transform.position +
                                   Vector3.Scale(Player.instance.hmdTransform.forward.normalized, new Vector3(5, 0, 5)),
                                   Quaternion.identity));
        }

        public void Update()
        {
            _subject.OnNext(Unit.Default);
        }
    }
}