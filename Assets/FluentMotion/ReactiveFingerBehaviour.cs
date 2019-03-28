using Leap;
using Leap.Unity;
using System;
using UniRx;
using UnityEngine;

namespace Assets.FluentMotion
{
    public abstract class ReactiveFingerBehaviour : MonoBehaviour
    {
        public FingerModel Finger;
        private Subject<FingerModel> Subject = new Subject<FingerModel>();

        public void Update() => Subject.OnNext(Finger);

        public IObservable<Finger> ReactiveFinger => Subject.Select(finger => finger.GetLeapFinger());
    }
}
