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
        private const float MinimumSpeed = 0.1f;

        private static bool And(bool a, bool b) => a && b;
        private static bool Or(bool a, bool b) => a || b;

        private static IObservable<TInput> When<TInput, TResult>(this IObservable<TInput> @this,
                                                                 Func<TInput, TResult> selector,
                                                                 params Func<TResult, bool>[] conditions) =>
            @this.Where(elem => conditions
                               .Select(f => f(selector(elem)))
                               .Aggregate(And));

        [UsedImplicitly]
        public static IObservable<T> When<T>(this IObservable<T> @this,
                                             params Func<T, bool>[] conditions) =>
            @this.When(x => x, conditions);

        #region hand

        [UsedImplicitly]
        public static IObservable<IList<Finger>> Fingers(this IObservable<Hand> @this,
                                                         params Finger.FingerType[] fingerTypes) =>
            @this.Select(hand => fingerTypes
                                .Select(hand.GetFinger)
                                .ToList());

        [UsedImplicitly]
        public static IObservable<Finger> Finger(this IObservable<Hand> @this,
                                                 Finger.FingerType fingerType) =>
            @this.Select(It.GetFinger(fingerType));

        [UsedImplicitly]
        public static IObservable<IList<Finger>> Fingers(this IObservable<Hand> @this) =>
            @this.Select(hand => hand.Fingers);

        [UsedImplicitly]
        public static IObservable<Hand> AreExtended(this IObservable<Hand> @this,
                                                    params Finger.FingerType[] fingerTypes) =>
            @this.When(hand => fingerTypes.Select(hand.GetFinger), All.AreExtended);

        [UsedImplicitly]
        public static IObservable<Finger> Thumb(this IObservable<Hand> @this) => @this.Select(It.GetThumb);

        public static IObservable<Hand> Thumb(this IObservable<Hand> @this,
                                              params Func<Finger, bool>[] conditions) =>
            @this.When(It.GetThumb, conditions);

        [UsedImplicitly]
        public static IObservable<Finger> Index(this IObservable<Hand> @this) => @this.Select(It.GetIndex);

        [UsedImplicitly]
        public static IObservable<Hand> Index(this IObservable<Hand> @this,
                                              params Func<Finger, bool>[] conditions) =>
            @this.When(It.GetIndex, conditions);

        [UsedImplicitly]
        public static IObservable<Finger> Middle(this IObservable<Hand> @this) =>
            @this.Select(It.GetMiddle);

        [UsedImplicitly]
        public static IObservable<Hand> Middle(this IObservable<Hand> @this,
                                               params Func<Finger, bool>[] conditions) =>
            @this.When(It.GetMiddle, conditions);

        [UsedImplicitly]
        public static IObservable<Finger> Ring(this IObservable<Hand> @this) =>
            @this.Select(It.GetRing);

        [UsedImplicitly]
        public static IObservable<Hand> Ring(this IObservable<Hand> @this,
                                             params Func<Finger, bool>[] conditions) =>
            @this.When(It.GetRing, conditions);

        [UsedImplicitly]
        public static IObservable<Finger> Pinky(this IObservable<Hand> @this) =>
            @this.Select(It.GetPinky);

        [UsedImplicitly]
        public static IObservable<Hand> Pinky(this IObservable<Hand> @this,
                                              params Func<Finger, bool>[] conditions) =>
            @this.When(It.GetPinky, conditions);

        [UsedImplicitly]
        public static IObservable<Hand> IsPinching(this IObservable<Hand> @this) =>
            @this.Where(It.IsPinching);

        /// <summary>
        ///     <para>Check if the hand is pointing to a given <paramref name="target"/>.</para>
        /// </summary>
        /// <param name="this">The observable instance</param>
        /// <param name="player">The player instance</param>
        /// <param name="positionSelector">A function to map the <paramref name="player"/> to a Vector4 instance (usually, the position)</param>
        /// <param name="target">The targeted object</param>
        /// <param name="targetPositionSelector">A function to map the <paramref name="target"/> to a Vector4</param>
        /// <param name="tolerance">A function returning the maximum accepted error of the dot product between the hand's palm's normal and the
        ///     vector between the target's position and the player's position</param>
        [UsedImplicitly]
        public static IObservable<Hand> PalmIsFacing<TPlayer, TTarget>(this IObservable<Hand> @this,
                                                                       [NotNull] TPlayer player,
                                                                       Func<TPlayer, Vector4> positionSelector,
                                                                       [NotNull] TTarget target,
                                                                       Func<TTarget, Vector4> targetPositionSelector,
                                                                       Func<float> tolerance) =>
            @this.Where(Palm.IsFacing(Direction.From(player, positionSelector).To(target, targetPositionSelector))
                            .WithTolerance(tolerance()));

        public static IObservable<Hand> PalmIsFacing<TPlayer, TTarget>(this IObservable<Hand> @this,
                                                                       [NotNull] TPlayer player,
                                                                       Func<TPlayer, Vector4> positionSelector,
                                                                       [NotNull] TTarget target,
                                                                       Func<TTarget, Vector4> targetPositionSelector,
                                                                       float tolerance = DotProductTolerance) =>
            @this.PalmIsFacing(player, positionSelector, target, targetPositionSelector, () => tolerance);

        [UsedImplicitly]
        public static IObservable<Hand> PalmIsFacing<TTarget>(this IObservable<Hand> @this,
                                                              [NotNull] TTarget target,
                                                              Func<TTarget, Vector4> selector,
                                                              Func<float> tolerance) =>
            @this.PalmIsFacing(Player.instance, player => player.transform.position, target, selector, tolerance);

        public static IObservable<Hand> PalmIsFacing<TTarget>(this IObservable<Hand> @this,
                                                              [NotNull] TTarget target,
                                                              Func<TTarget, Vector4> selector,
                                                              float tolerance = DotProductTolerance) =>
            @this.PalmIsFacing(target, selector, () => tolerance);

        [UsedImplicitly]
        public static IObservable<Hand> IsFist(this IObservable<Hand> @this) =>
            @this.Where(It.IsFist);

        /// <summary>
        ///     <para>Check if the hand is moving.</para>
        ///     <para>To specify a target direction, use <see cref="IsMoving{TReference}(IObservable{Hand}, TReference, Func{TReference, Vector4}, float, float)"/></para>
        /// </summary>
        /// <param name="this"></param>
        /// <param name="speed"></param>
        /// <returns><see cref="IObservable{T}"/></returns>
        [UsedImplicitly]
        public static IObservable<Hand> IsMoving(this IObservable<Hand> @this,
                                                 float speed = MinimumSpeed) =>
            @this.Where(It.IsMoving().WithSpeed(speed));

        /// <summary>
        ///     <para>Check if the hand is moving in a given direction.</para>
        ///     <para>To check if the hand is moving (without caring about the direction), see <seealso cref="IsMoving(IObservable{Hand}, float)"/>.</para>
        /// </summary>
        /// <param name="this">The observable instance.</param>
        /// <param name="reference">Object whose axis to use as reference.</param>
        /// <param name="to">Function to select the direction from <paramref name="reference"/> to move in.</param>
        /// <param name="speed">The minimum speed required to detect a movement of the hand (in m/s).</param>
        /// <param name="tolerance">The maximum error of the dot product between the hand's direction and the target's position.</param>
        /// <returns><see cref="IObservable{T}"/></returns>
        [UsedImplicitly]
        public static IObservable<Hand> IsMoving<TReference>(this IObservable<Hand> @this,
                                                             [NotNull] TReference reference,
                                                             [NotNull] Func<TReference, Vector4> to,
                                                             float speed = MinimumSpeed,
                                                             float tolerance = DotProductTolerance) =>
            @this.IsMoving(speed).Where(hand => Direction.Of<Hand>(h => h.PalmVelocity.ToVector3())
                                                         .Is(reference, to)
                                                         .WithTolerance(DotProductTolerance)(hand));

        public static IObservable<Hand> IsMoving<TReference>(this IObservable<Hand> @this,
                                                             [NotNull] TReference reference,
                                                             SwipeDirection direction,
                                                             float speed = MinimumSpeed,
                                                             float tolerance = DotProductTolerance)
            where TReference : Component
        {
            switch (direction)
            {
                case SwipeDirection.Left:
                    return @this.IsMovingLeft(reference, speed, tolerance);
                case SwipeDirection.Right:
                    return @this.IsMovingRight(reference, speed, tolerance);
                case SwipeDirection.Up:
                    return @this.IsMovingUp(reference, speed, tolerance);
                case SwipeDirection.Down:
                    return @this.IsMovingDown(reference, speed, tolerance);
                case SwipeDirection.Forward:
                    return @this.IsMovingForward(reference, speed, tolerance);
                case SwipeDirection.Backward:
                    return @this.IsMovingBackward(reference, speed, tolerance);
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction,"Please specify a valid direction");
            }
        }

        [UsedImplicitly]
        public static IObservable<Hand> IsMovingRight(this IObservable<Hand> @this,
                                                      [NotNull] Player player,
                                                      float speed = MinimumSpeed,
                                                      float tolerance = DotProductTolerance) =>
            @this.IsMoving(player, x => x.hmdTransform.right, speed, tolerance);

        /// <summary>
        ///     <para>Check if the hand is moving RIGHT relative to <paramref name="reference"/>.</para>
        ///     For tracking the HMD in VR applications, see <see cref="IsMovingRight"/>.
        /// </summary>
        /// <param name="this">The observable instance.</param>
        /// <param name="reference">Object whose RIGHT axis is used as reference.</param>
        /// <param name="speed">The minimum speed required to detect a movement of the hand (in m/s).</param>
        /// <param name="tolerance">The maximum error of the dot product between the hand's direction and the target's position.</param>
        /// <typeparam name="TReference">The type of <paramref name="reference"/></typeparam>
        /// <returns><see cref="IObservable{T}"/></returns>
        [UsedImplicitly]
        public static IObservable<Hand> IsMovingRight<TReference>(this IObservable<Hand> @this,
                                                                  [NotNull] TReference reference,
                                                                  float speed = MinimumSpeed,
                                                                  float tolerance = DotProductTolerance)
            where TReference : Component =>
            @this.IsMoving(reference, x => x.transform.right, speed, tolerance);

        [UsedImplicitly]
        public static IObservable<Hand> IsMovingLeft(this IObservable<Hand> @this,
                                                     Player player,
                                                     float speed = MinimumSpeed,
                                                     float tolerance = DotProductTolerance) =>
            @this.IsMoving(player, x => -x.hmdTransform.right, speed, tolerance);

        [UsedImplicitly]
        public static IObservable<Hand> IsMovingLeft<TReference>(this IObservable<Hand> @this,
                                                                 [NotNull] TReference reference,
                                                                 float speed = MinimumSpeed,
                                                                 float tolerance = DotProductTolerance)
            where TReference : Component =>
            @this.IsMoving(reference, x => -x.transform.right, speed, tolerance);

        [UsedImplicitly]
        public static IObservable<Hand> IsMovingUp(this IObservable<Hand> @this,
                                                   Player player,
                                                   float speed = MinimumSpeed,
                                                   float tolerance = DotProductTolerance) =>
            @this.IsMoving(player, x => x.hmdTransform.up, speed, tolerance);

        [UsedImplicitly]
        public static IObservable<Hand> IsMovingUp<TReference>(this IObservable<Hand> @this,
                                                               [NotNull] TReference reference,
                                                               float speed = MinimumSpeed,
                                                               float tolerance = DotProductTolerance)
            where TReference : Component =>
            @this.IsMoving(reference, x => x.transform.up, speed, tolerance);

        [UsedImplicitly]
        public static IObservable<Hand> IsMovingDown(this IObservable<Hand> @this,
                                                     Player player,
                                                     float speed = MinimumSpeed,
                                                     float tolerance = DotProductTolerance) =>
            @this.IsMoving(player, x => -x.hmdTransform.up, speed, tolerance);

        [UsedImplicitly]
        public static IObservable<Hand> IsMovingDown<TReference>(this IObservable<Hand> @this,
                                                                 [NotNull] TReference reference,
                                                                 float speed = MinimumSpeed,
                                                                 float tolerance = DotProductTolerance)
            where TReference : Component =>
            @this.IsMoving(reference, x => -x.transform.up, speed, tolerance);

        [UsedImplicitly]
        public static IObservable<Hand> IsMovingForward(this IObservable<Hand> @this,
                                                        Player player,
                                                        float speed = MinimumSpeed,
                                                        float tolerance = DotProductTolerance) =>
            @this.IsMoving(player, x => x.hmdTransform.forward, speed, tolerance);

        [UsedImplicitly]
        public static IObservable<Hand> IsMovingForward<TReference>(this IObservable<Hand> @this,
                                                                    [NotNull] TReference reference,
                                                                    float speed = MinimumSpeed,
                                                                    float tolerance = DotProductTolerance)
            where TReference : Component =>
            @this.IsMoving(reference, x => x.transform.forward, speed, tolerance);

        [UsedImplicitly]
        public static IObservable<Hand> IsMovingBackward(this IObservable<Hand> @this,
                                                         Player player,
                                                         float speed = MinimumSpeed,
                                                         float tolerance = DotProductTolerance) =>
            @this.IsMoving(player, x => -x.hmdTransform.forward, speed, tolerance);

        [UsedImplicitly]
        public static IObservable<Hand> IsMovingBackward<TReference>(this IObservable<Hand> @this,
                                                                     [NotNull] TReference reference,
                                                                     float speed = MinimumSpeed,
                                                                     float tolerance = DotProductTolerance)
            where TReference : Component =>
            @this.IsMoving(reference, x => -x.transform.forward, speed, tolerance);

        #endregion

        #region finger

        public static IObservable<Finger> IsExtended(this IObservable<Finger> @this) =>
            @this.Where(It.IsExtended);

        public static IObservable<Finger> IsPointingTo<TPlayer, TTarget>(this IObservable<Finger> @this,
                                                                         TPlayer player,
                                                                         Func<TPlayer, Vector4> positionSelector,
                                                                         [NotNull] TTarget target,
                                                                         Func<TTarget, Vector4> targetPositionSelector,
                                                                         float tolerance = DotProductTolerance) =>
            @this.Where(It.IsPointingTo(Direction.From(player, positionSelector).To(target, targetPositionSelector))
                          .WithTolerance(tolerance));

        private static void foo(IObservable<Finger> Finger)
        {
            GameObject targetObject = new GameObject();
            Finger.IsPointingTo(Player.instance,
                                player => player.hmdTransform.position,
                                targetObject,
                                target => target.transform.position
                                )
                  .Subscribe(finger => DoSomething(finger));
        }

        private static void DoSomething<T>(T t)
        {
        }

        public static IObservable<Finger> IsPointingTo<TTarget>(this IObservable<Finger> @this,
                                                                [NotNull] TTarget target,
                                                                Func<TTarget, Vector4> selector,
                                                                float tolerance = DotProductTolerance) =>
            @this.IsPointingTo(Player.instance, player => player.transform.position, target, selector, tolerance);

        #endregion

        #region hands

        [UsedImplicitly]
        public static IObservable<TResult> Select<TResult>(this IObservable<ReactiveHandsHelper<Hand>> @this,
                                                           Func<Hand, Hand, TResult> selector) =>
            @this.Select(hands => selector(hands.LeftHand, hands.RightHand));

        [UsedImplicitly]
        public static IObservable<ReactiveHandsHelper<Hand>> Where(this IObservable<ReactiveHandsHelper<Hand>> @this,
                                                                   Func<Hand, Hand, bool> predicate) =>
            @this.Where(hands => predicate(hands.LeftHand, hands.RightHand));

        public static IDisposable Subscribe(this IObservable<ReactiveHandsHelper<Hand>> @this,
                                            Action<Hand, Hand> action) =>
            @this.Subscribe(hands => action(hands.LeftHand, hands.RightHand));

        [UsedImplicitly]
        public static IObservable<ReactiveHandsHelper<Hand>> PalmsAreFacing<TLeftTarget, TRightTarget>(
            this IObservable<ReactiveHandsHelper<Hand>> @this,
            [NotNull] TLeftTarget leftTarget,
            Func<TLeftTarget, Vector4> selectorLeft,
            [NotNull] TRightTarget rightTarget,
            Func<TRightTarget, Vector4> selectorRight,
            float tolerance = DotProductTolerance) =>
            @this.Where((left, right) => Direction.Of<Hand>(hand => hand.PalmNormal.ToVector3())
                                                  .Is(leftTarget, selectorLeft).WithTolerance(tolerance)(left) &&
                                         Direction.Of<Hand>(hand => hand.PalmNormal.ToVector3())
                                                  .Is(rightTarget, selectorRight).WithTolerance(tolerance)(right));

        [UsedImplicitly]
        public static IObservable<ReactiveHandsHelper<Hand>> PalmsAreFacing<TTarget>(this IObservable<ReactiveHandsHelper<Hand>> @this,
                                                                                     [NotNull] TTarget target,
                                                                                     Func<TTarget, Vector4> selector,
                                                                                     float tolerance =
                                                                                         DotProductTolerance) =>
            @this.PalmsAreFacing(target, selector, target, selector, tolerance);

        [UsedImplicitly]
        public static IObservable<ReactiveHandsHelper<Hand>> PalmsAreFacing(this IObservable<ReactiveHandsHelper<Hand>> @this,
                                                                            float tolerance =
                                                                                DotProductTolerance) =>
            @this.Where((left, right) => Direction.Of<Hand>(hand => hand.PalmNormal.ToVector3())
                                                  .Is(right, hand => -hand.PalmNormal.ToVector3())
                                                  .WithTolerance(tolerance)(left));

        [UsedImplicitly]
        public static IObservable<ReactiveHandsHelper<Hand>> AreMakingFists(this IObservable<ReactiveHandsHelper<Hand>> @this) =>
            @this.Where((left, right) => It.IsFist(left) && It.IsFist(right));

        public static IObservable<ReactiveHandsHelper<Hand>> AreMoving(this IObservable<ReactiveHandsHelper<Hand>> @this,
                                                                       Func<float> speed) =>
            @this.Where((left, right) => It.IsMoving().WithSpeed(speed())(left) && It.IsMoving().WithSpeed(speed())(right));

        public static IObservable<ReactiveHandsHelper<Hand>> AreMoving(this IObservable<ReactiveHandsHelper<Hand>> @this,
                                                                       float speed = MinimumSpeed) =>
            @this.AreMoving(() => speed);

        public static IObservable<ReactiveHandsHelper<Hand>> AreMoving<TReference>(this IObservable<ReactiveHandsHelper<Hand>> @this,
                                                                                   TReference reference,
                                                                                   Func<TReference, Vector4> selector,
                                                                                   Func<float> speed,
                                                                                   Func<float> tolerance) =>
            @this.AreMoving(speed).Where((left, right) => Direction.Of<Hand>(hand => hand.PalmVelocity.ToVector3())
                                                                   .Is(reference, selector).WithTolerance(tolerance())(left) &&
                                                          Direction.Of<Hand>(hand => hand.PalmVelocity.ToVector3())
                                                                   .Is(reference, selector).WithTolerance(tolerance())(right));

        public static IObservable<ReactiveHandsHelper<Hand>> AreMoving<TReference>(this IObservable<ReactiveHandsHelper<Hand>> @this,
                                                                                   TReference reference,
                                                                                   Func<TReference, Vector4> selector,
                                                                                   float speed =
                                                                                       MinimumSpeed,
                                                                                   float tolerance =
                                                                                       DotProductTolerance) =>
            @this.AreMoving(reference, selector, () => speed, () => tolerance);

        #endregion
    }
}