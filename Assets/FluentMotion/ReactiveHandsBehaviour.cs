using System;
using FluentMotion.hand;
using FluentMotion.helpers;
using Leap;
using UnityEngine;

namespace FluentMotion
{
    public class ReactiveHandsBehaviour : MonoBehaviour
    {
        protected IObservable<ReactiveHandsHelper<Hand>> Hands => gameObject.GetComponent<ReactiveHands>().AsObservable();
    }
}