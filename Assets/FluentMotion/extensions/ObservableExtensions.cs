using System;
using System.Collections.Generic;
using System.Linq;
using FluentMotion.helpers;
using FluentMotion.helpers.hand;
using JetBrains.Annotations;
using Leap;
using Leap.Unity;
using Leap.Unity.Infix;
using UniRx;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Hand = Leap.Hand;

namespace FluentMotion.extensions
{
    public static class ObservableExtensions
    {
        private const float DotProductTolerance = 0.075f;
        private const float MinimumSpeed        = 0.1f;

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

        /// <summary>
        /// Check if the hand is pointing to a given vector.
        /// </summary>
        /// <param name="this"></param>
        /// <param name="target">The point in world coordinates to which the hand should point</param>
        /// <param name="tolerance">The maximum error of the dot product between the hand's direction and the target's position</param>
        public static IObservable<Hand> IsPointingTo(this IObservable<Hand> @this,
                                                     Vector4                target,
                                                     float                  tolerance = DotProductTolerance) =>
            @this.Where(hand => hand.Direction.ToVector3().Dot(target).Equals(1, tolerance));

        public static IObservable<Hand> IsPointingTo(this IObservable<Hand> @this,
                                                     Vector                 target,
                                                     float                  tolerance = DotProductTolerance) =>
            @this.IsPointingTo(target.ToVector3(), tolerance);

        public static IObservable<Hand> IsPointingTo(this IObservable<Hand> @this,
                                                     GameObject             other,
                                                     float                  tolerance = DotProductTolerance) =>
            @this.IsPointingTo(other.transform.position, tolerance);

        public static IObservable<Hand> IsPointingTo(this IObservable<Hand> @this,
                                                     Hand                   other,
                                                     float                  tolerance = DotProductTolerance) =>
            @this.IsPointingTo(other.PalmPosition.ToVector3(), tolerance);

        public static IObservable<Hand> IsFist(this IObservable<Hand> @this) =>
            @this.Where(hand => hand.GetFistStrength().Equals(1, float.Epsilon));

        public static IObservable<Hand> IsMoving(this IObservable<Hand> @this,
                                                 float                  speed = MinimumSpeed) =>
            @this.Where(hand => hand.PalmVelocity.ToVector3().magnitude > speed);

        /// <summary>
        /// Check if the hand is moving towards a given point
        /// </summary>
        /// <param name="this"></param>
        /// <param name="reference"></param>
        /// <param name="to">Point in world coordinates</param>
        /// <param name="speed">The minimum speed required to detect a movement of the hand</param>
        /// <param name="tolerance">The maximum error of the dot product between the hand's direction and the target's position</param>
        /// <returns></returns>
        public static IObservable<Hand> IsMoving<TReference>(this      IObservable<Hand>         @this,
                                                             [NotNull] TReference                reference,
                                                             [NotNull] Func<TReference, Vector4> to,
                                                             float                               speed     = MinimumSpeed,
                                                             float                               tolerance = DotProductTolerance) =>
            @this.IsMoving(speed).Where(hand => hand.PalmVelocity
                                                    .ToVector3()
                                                    .normalized
                                                    .Dot(to(reference).normalized)
                                                    .Equals(1, tolerance));

        public static IObservable<Hand> IsMovingRight(this      IObservable<Hand> @this,
                                                      [NotNull] Player            player,
                                                      float                       speed     = MinimumSpeed,
                                                      float                       tolerance = DotProductTolerance) =>
            @this.IsMoving(player, x => x.hmdTransform.right, speed, tolerance);

        public static IObservable<Hand> IsMovingRight<TReference>(this      IObservable<Hand> @this,
                                                                  [NotNull] TReference        reference,
                                                                  float                       speed     = MinimumSpeed,
                                                                  float                       tolerance = DotProductTolerance)
            where TReference : Component =>
            @this.IsMoving(reference, x => x.transform.right, speed, tolerance);

        public static IObservable<Hand> IsMovingLeft(this IObservable<Hand> @this,
                                                     Player                 player,
                                                     float                  speed     = MinimumSpeed,
                                                     float                  tolerance = DotProductTolerance) =>
            @this.IsMoving(player, x => -x.hmdTransform.right, speed, tolerance);

        public static IObservable<Hand> IsMovingLeft<TReference>(this      IObservable<Hand> @this,
                                                                 [NotNull] TReference        reference,
                                                                 float                       speed     = MinimumSpeed,
                                                                 float                       tolerance = DotProductTolerance)
            where TReference : Component =>
            @this.IsMoving(reference, x => -x.transform.right, speed, tolerance);

        public static IObservable<Hand> IsMovingUp(this IObservable<Hand> @this,
                                                   Player                 player,
                                                   float                  speed     = MinimumSpeed,
                                                   float                  tolerance = DotProductTolerance) =>
            @this.IsMoving(player, x => x.hmdTransform.up, speed, tolerance);

        public static IObservable<Hand> IsMovingUp<TReference>(this      IObservable<Hand> @this,
                                                               [NotNull] TReference        reference,
                                                               float                       speed     = MinimumSpeed,
                                                               float                       tolerance = DotProductTolerance)
            where TReference : Component =>
            @this.IsMoving(reference, x => x.transform.up, speed, tolerance);

        public static IObservable<Hand> IsMovingDown(this IObservable<Hand> @this,
                                                     Player                 player,
                                                     float                  speed     = MinimumSpeed,
                                                     float                  tolerance = DotProductTolerance) =>
            @this.IsMoving(player, x => - x.hmdTransform.up, speed, tolerance);

        public static IObservable<Hand> IsMovingDown<TReference>(this      IObservable<Hand> @this,
                                                                 [NotNull] TReference        reference,
                                                                 float                       speed     = MinimumSpeed,
                                                                 float                       tolerance = DotProductTolerance)
            where TReference : Component =>
            @this.IsMoving(reference, x => - x.transform.up, speed, tolerance);

        public static IObservable<Hand> IsMovingForward(this IObservable<Hand> @this,
                                                        Player                 player,
                                                        float                  speed     = MinimumSpeed,
                                                        float                  tolerance = DotProductTolerance) =>
            @this.IsMoving(player, x => x.hmdTransform.forward, speed, tolerance);

        public static IObservable<Hand> IsMovingForward<TReference>(this      IObservable<Hand> @this,
                                                                    [NotNull] TReference        reference,
                                                                    float                       speed     = MinimumSpeed,
                                                                    float                       tolerance = DotProductTolerance)
            where TReference : Component =>
            @this.IsMoving(reference, x => x.transform.forward, speed, tolerance);

        public static IObservable<Hand> IsMovingBackward(this IObservable<Hand> @this,
                                                         Player                 player,
                                                         float                  speed     = MinimumSpeed,
                                                         float                  tolerance = DotProductTolerance) =>
            @this.IsMoving(player, x => - x.hmdTransform.forward, speed, tolerance);

        public static IObservable<Hand> IsMovingBackward<TReference>(this      IObservable<Hand> @this,
                                                                     [NotNull] TReference        reference,
                                                                     float                       speed     = MinimumSpeed,
                                                                     float                       tolerance = DotProductTolerance)
            where TReference : Component =>
            @this.IsMoving(reference, x => - x.transform.forward, speed, tolerance);

        #endregion

        #region finger

        public static IObservable<Finger> IsExtended(this IObservable<Finger> @this) =>
            @this.Where(finger => finger.IsExtended);

        public static IObservable<Finger> IsPointingTo(this IObservable<Finger> @this,
                                                       Vector4                  to,
                                                       float                    tolerance = DotProductTolerance) =>
            @this.Where(finger => finger.Direction.ToVector3().Dot(to).Equals(1, tolerance));

        public static IObservable<Finger> IsPointingTo(this IObservable<Finger> @this,
                                                       Vector                   to,
                                                       float                    tolerance = DotProductTolerance) =>
            @this.IsPointingTo(to.ToVector3(), tolerance);

        public static IObservable<Finger> IsPointingTo(this IObservable<Finger> @this,
                                                       GameObject               to,
                                                       float                    tolerance = DotProductTolerance) =>
            @this.IsPointingTo(to.transform.position, tolerance);

        public static IObservable<Finger> IsPointingTo(this IObservable<Finger> @this,
                                                       Finger                   to,
                                                       float                    tolerance = DotProductTolerance) =>
            @this.IsPointingTo(to.TipPosition, tolerance);

        public static IObservable<Finger> IsPointingTo(this IObservable<Finger> @this,
                                                       Hand                     to,
                                                       float                    tolerance = DotProductTolerance) =>
            @this.IsPointingTo(to.PalmPosition, tolerance);

        #endregion

        #region hands

        public static IObservable<TResult> Select<TResult>(this IObservable<ReactiveHandsHelper<Hand>> @this,
                                                           Func<Hand, Hand, TResult>                   selector) =>
            @this.Select(hands => selector(hands.LeftHand, hands.RightHand));

        public static IObservable<ReactiveHandsHelper<Hand>> Where(this IObservable<ReactiveHandsHelper<Hand>> @this,
                                                                   Func<Hand, Hand, bool>                      predicate) =>
            @this.Where(hands => predicate(hands.LeftHand, hands.RightHand));

        public static IObservable<ReactiveHandsHelper<Hand>> AreFacing(this IObservable<ReactiveHandsHelper<Hand>> @this,
                                                                       Vector4                                     target,
                                                                       float                                       tolerance = DotProductTolerance) =>
            @this.Where((left, right) =>
                            left.Direction.ToVector3().Dot(target).Equals(1, tolerance) &&
                            right.Direction.ToVector3().Dot(target).Equals(1, tolerance));

        public static IObservable<ReactiveHandsHelper<Hand>> AreFacing(this IObservable<ReactiveHandsHelper<Hand>> @this,
                                                                       Vector                                      target,
                                                                       float                                       tolerance = DotProductTolerance) =>
            @this.AreFacing(target.ToVector3(), tolerance);

        public static IObservable<ReactiveHandsHelper<Hand>> AreFacing(this IObservable<ReactiveHandsHelper<Hand>> @this,
                                                                       GameObject                                  target,
                                                                       float                                       tolerance = DotProductTolerance) =>
            @this.AreFacing(target.transform.position, tolerance);

        public static IObservable<ReactiveHandsHelper<Hand>> AreFacing(this IObservable<ReactiveHandsHelper<Hand>> @this,
                                                                       float                                       tolerance = DotProductTolerance) =>
            @this.Where((left, right) => left.Direction.ToVector3().Dot(right.Direction.ToVector3()).Equals((float) Math.PI, tolerance));

        public static IObservable<ReactiveHandsHelper<Hand>> AreMakingFists(this IObservable<ReactiveHandsHelper<Hand>> @this) =>
            @this.Where((left, right) => left.GetFistStrength().Equals(1, float.Epsilon) && right.GetFistStrength().Equals(1, float.Epsilon));

        #endregion
    }
}