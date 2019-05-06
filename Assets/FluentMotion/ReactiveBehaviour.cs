using System;
using FluentMotion.hand;
using Leap;
using UnityEngine;

namespace FluentMotion
{
    public class ReactiveHandBehaviour : MonoBehaviour
    {
        protected IObservable<Hand> Hand => gameObject.GetComponent<ReactiveHand>().AsObservable();
    }
}