using Assets.FluentMotion;
using Assets.FluentMotion.helpers;
using Assets.FluentMotion.helpers.FingerHelpers;
using Assets.FluentMotion.helpers.HandHelpers;
using Leap.Unity;
using System;
using System.Linq;
using UniRx;
using UnityEngine;

namespace Assets
{
    class FingerExtendedDemo : ReactiveHandBehaviour
    {
        public void Start()
        {
            this.WhenThumb(It.IsExtended)
                .Sample(TimeSpan.FromSeconds(2))
                .Subscribe(_ => Debug.Log("Am extended!"));
        }

    }
}
