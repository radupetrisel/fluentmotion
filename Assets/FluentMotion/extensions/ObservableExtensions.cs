using System;
using System.Collections.Generic;
using System.Linq;
using FluentMotion.helpers.hand;
using Leap;
using Leap.Unity;
using UniRx;
using UnityEngine;

namespace FluentMotion.extensions
{
    public static class ObservableExtensions
    {
        private const float AngleTolerance = (float) (Math.PI / 12);

        private static bool And(bool a, bool b) => a && b;

        private static IObservable<TInput> When<TInput, TResult>(this IObservable<TInput>     @this,
                                                                 Func<TInput, TResult>        selector,
                                                                 params Func<TResult, bool>[] conditions) =>
            @this.Where(elem => conditions
                               .Select(f => f(selector(elem)))
                               .Aggregate(And));

        public static IObservable<T> When<T>(this   IObservable<T>  @this,
                                             params Func<T, bool>[] conditions) =>
            @this.When(x => x, conditions);

        #region hand

        public static IObservable<Finger> FromHand(this IObservable<Hand> @this,
                                                   Func<Hand, Finger>     select) =>
            @this.Select(select);

        public static IObservable<IList<Finger>> Fingers(this IObservable<Hand> @this) =>
            @this.Select(hand => hand.Fingers);

        public static IObservable<Hand> IsExtended(this   IObservable<Hand>   @this,
                                                   params Finger.FingerType[] fingerTypes) =>
            @this.When(hand => fingerTypes.Select(hand.GetFinger), fingers => fingers.All(finger => finger.IsExtended));

        public static IObservable<Finger> Thumb(this IObservable<Hand> @this) => @this.Select(hand => hand.GetThumb());

        public static IObservable<Hand> Thumb(this   IObservable<Hand>    @this,
                                              params Func<Finger, bool>[] conditions) =>
            @this.When(hand => hand.GetThumb(), conditions);

        public static IObservable<Finger> Index(this IObservable<Hand> @this) => @this.Select(hand => hand.GetIndex());

        public static IObservable<Hand> Index(this   IObservable<Hand>    @this,
                                              params Func<Finger, bool>[] conditions) =>
            @this.When(hand => hand.GetIndex(), conditions);

        public static IObservable<Finger> Middle(this IObservable<Hand> @this) =>
            @this.Select(hand => hand.GetMiddle());

        public static IObservable<Hand> Middle(this   IObservable<Hand>    @this,
                                               params Func<Finger, bool>[] conditions) =>
            @this.When(hand => hand.GetMiddle(), conditions);

        public static IObservable<Finger> Ring(this IObservable<Hand> @this) => @this.Select(hand => hand.GetRing());

        public static IObservable<Hand> Ring(this   IObservable<Hand>    @this,
                                             params Func<Finger, bool>[] conditions) =>
            @this.When(hand => hand.GetRing(), conditions);

        public static IObservable<Finger> Pinky(this IObservable<Hand> @this) => @this.Select(hand => hand.GetPinky());

        public static IObservable<Hand> Pinky(this   IObservable<Hand>    @this,
                                              params Func<Finger, bool>[] conditions) =>
            @this.When(hand => hand.GetPinky(), conditions);

        public static IObservable<Hand> IsPinching(this IObservable<Hand> @this) =>
            @this.Where(hand => hand.IsPinching());

        public static IObservable<Hand> IsPointingTo(this IObservable<Hand> @this,
                                                     Vector                 target,
                                                     float                  angle = AngleTolerance) =>
            @this.Where(hand => hand.Direction.AngleTo(target).NearlyEquals(0, angle));

        public static IObservable<Hand> IsPointingTo(this IObservable<Hand> @this,
                                                     Vector4                target,
                                                     float                  angle = AngleTolerance) =>
            @this.IsPointingTo(target.ToVector3().ToVector(), angle);

        public static IObservable<Hand> IsPointingTo(this IObservable<Hand> @this,
                                                     GameObject             other,
                                                     float                  angle = AngleTolerance) =>
            @this.IsPointingTo(other.transform.position, angle);

        public static IObservable<Hand> IsPointingTo(this IObservable<Hand> @this,
                                                     Hand                   other,
                                                     float                  angle = AngleTolerance) =>
            @this.IsPointingTo(other.PalmPosition.ToVector4(), angle);

        public static IObservable<Hand> IsFist(this IObservable<Hand> @this) =>
            @this.Where(hand => hand.GetFistStrength().NearlyEquals(1, float.Epsilon));

        public static IObservable<Hand> IsMoving(this IObservable<Hand> @this) =>
            @this.Where(hand => hand.PalmVelocity.Magnitude > 0);

        public static IObservable<Hand> IsMoving(this IObservable<Hand> @this,
                                                 Vector                 to,
                                                 float                  angle = AngleTolerance) =>
            @this.IsMoving().Where(hand => hand.PalmVelocity.AngleTo(to).NearlyEquals(0, angle));

        public static IObservable<Hand> IsMoving(this IObservable<Hand> @this,
                                                 Vector4                to,
                                                 float                  angle = AngleTolerance) =>
            @this.IsMoving(to.ToVector3().ToVector(), angle);

        public static IObservable<Hand> IsMoving(this IObservable<Hand> @this,
                                                 Vector3                to,
                                                 float                  angle = AngleTolerance) =>
            @this.IsMoving(to.ToVector(), angle);

        public static IObservable<Hand> IsMovingRight(this IObservable<Hand> @this,
                                                      Camera                 camera,
                                                      float                  angle = AngleTolerance) =>
            @this.IsMoving(camera.transform.right, angle);

        public static IObservable<Hand> IsMovingRight(this IObservable<Hand> @this,
                                                      float                  angle = AngleTolerance) =>
            @this.IsMovingRight(Camera.current, angle);

        public static IObservable<Hand> IsMovingLeft(this IObservable<Hand> @this,
                                                     Camera                 camera,
                                                     float                  angle = AngleTolerance) =>
            @this.IsMoving(-camera.transform.right, angle);

        public static IObservable<Hand> IsMovingLeft(this IObservable<Hand> @this,
                                                     float                  angle = AngleTolerance) =>
            @this.IsMovingLeft(Camera.current, angle);

        public static IObservable<Hand> IsMovingUp(this IObservable<Hand> @this,
                                                   Camera                 camera,
                                                   float                  angle = AngleTolerance) =>
            @this.IsMoving(camera.transform.up, angle);

        public static IObservable<Hand> IsMovingUp(this IObservable<Hand> @this,
                                                   float                  angle = AngleTolerance) =>
            @this.IsMovingUp(Camera.current, angle);

        public static IObservable<Hand> IsMovingDown(this IObservable<Hand> @this,
                                                     Camera                 camera,
                                                     float                  angle = AngleTolerance) =>
            @this.IsMoving(-camera.transform.up, angle);

        public static IObservable<Hand> IsMovingDown(this IObservable<Hand> @this,
                                                     float                  angle = AngleTolerance) =>
            @this.IsMovingDown(Camera.current, angle);

        public static IObservable<Hand> IsMovingForward(this IObservable<Hand> @this,
                                                        Camera                 camera,
                                                        float                  angle = AngleTolerance) =>
            @this.IsMoving(camera.transform.forward, angle);

        public static IObservable<Hand> IsMovingForward(this IObservable<Hand> @this,
                                                        float                  angle = AngleTolerance) =>
            @this.IsMovingForward(Camera.current, angle);

        public static IObservable<Hand> IsMovingBackward(this IObservable<Hand> @this,
                                                         Camera                 camera,
                                                         float                  angle = AngleTolerance) =>
            @this.IsMoving(-camera.transform.forward, angle);

        public static IObservable<Hand> IsMovingBackward(this IObservable<Hand> @this,
                                                         float                  angle = AngleTolerance) =>
            @this.IsMovingBackward(Camera.current, angle);

        #endregion

        #region finger

        public static IObservable<Finger> IsExtended(this IObservable<Finger> @this) =>
            @this.Where(finger => finger.IsExtended);

        public static IObservable<Finger> IsPointingTo(this IObservable<Finger> @this,
                                                       Vector                   to,
                                                       float                    angle = AngleTolerance) =>
            @this.Where(finger => finger.Direction.AngleTo(to).NearlyEquals(0, angle));

        public static IObservable<Finger> IsPointingTo(this IObservable<Finger> @this,
                                                       Vector4                  to,
                                                       float                    angle = AngleTolerance) =>
            @this.IsPointingTo(to.ToVector3().ToVector(), angle);

        public static IObservable<Finger> IsPointingTo(this IObservable<Finger> @this,
                                                       GameObject               to,
                                                       float                    angle = AngleTolerance) =>
            @this.IsPointingTo(to.transform.position, angle);

        public static IObservable<Finger> IsPointingTo(this IObservable<Finger> @this,
                                                       Finger                   to,
                                                       float                    angle = AngleTolerance) =>
            @this.IsPointingTo(to.TipPosition, angle);

        public static IObservable<Finger> IsPointingTo(this IObservable<Finger> @this,
                                                       Hand                     to,
                                                       float                    angle = AngleTolerance) =>
            @this.IsPointingTo(to.PalmPosition, angle);

        #endregion

        #region hands

        

        #endregion
    }
}