using System;
using System.Collections;
using FluentMotion;
using FluentMotion.extensions;
using Leap.Unity;
using UniRx;
using UnityEngine;
using Hand = Leap.Hand;

namespace Demo
{
    public class PalmIsFacing : ReactiveHandBehaviour
    {
        public           LineRenderer   lineRenderer;
        private readonly WaitForSeconds _shotDuration = new WaitForSeconds(.1f);


        public void Start()
        {
            lineRenderer = GetComponent<LineRenderer>();

            Hand.When(hand => hand.Fingers.TrueForAll(finger => finger.IsExtended))
                .Sample(TimeSpan.FromSeconds(.5f))
                .Subscribe(Shoot);
        }

        private void Shoot(Hand hand)
        {
            StartCoroutine(ShotEffect());

            var rayOrigin = hand.PalmPosition.ToVector3();
            
            lineRenderer.SetPosition(0, rayOrigin);

            if (Physics.Raycast(rayOrigin, hand.PalmNormal.ToVector3(), out var hit, 50))
            {
                lineRenderer.SetPosition(1, hit.point);

                if (hit.rigidbody != null)
                {
                    hit.rigidbody.transform.localScale *= .9f;
                }
                else if (hit.collider != null)
                {
                    hit.collider.transform.localScale *= .9f;
                }
            }
            else
            {
                lineRenderer.SetPosition(1, hand.PalmNormal.Normalized.ToVector3() * 50);
            }
        }
        
        
        private IEnumerator ShotEffect()
        {
            lineRenderer.enabled = true;

            yield return _shotDuration;

            lineRenderer.enabled = false;
        }
    }
}