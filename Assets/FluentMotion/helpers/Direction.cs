using System;
using JetBrains.Annotations;
using Leap.Unity;
using Leap.Unity.Infix;
using UnityEngine;

namespace FluentMotion.helpers
{
    public static class Direction
    {
        public static DirectionTo To(Vector4 to) => new DirectionTo(to);

        public static DirectionTo To<TTarget>(TTarget                target,
                                              Func<TTarget, Vector4> selector) => To(selector(target));

        public static DirectionFrom From(Vector4 from) => new DirectionFrom(from);

        public static DirectionFrom From<TTarget>(TTarget                target,
                                                  Func<TTarget, Vector4> selector) => From(selector(target));

        public static DirectionOf<TReference> Of<TReference>(Func<TReference, Vector4> reference) => new DirectionOf<TReference>(reference);
    }

    public class DirectionFrom
    {
        private readonly Vector4 _from;

        public DirectionFrom(Vector4 from)
        {
            _from = from;
        }

        public Vector4 To(Vector4 to) => to - _from;

        public Vector4 To<TTarget>(TTarget                target,
                                   Func<TTarget, Vector4> selector) => To(selector(target));
    }

    public class DirectionTo
    {
        private readonly Vector4 _to;

        public DirectionTo(Vector4 to)
        {
            _to = to;
        }

        public Vector4 From(Vector4 from) => _to - from;

        public Vector4 From<TTarget>(TTarget                target,
                                     Func<TTarget, Vector4> selector) => From(selector(target));
    }

    public class DirectionOf<TReference>
    {
        private readonly Func<TReference, Vector4> _of;

        public DirectionOf(Func<TReference, Vector4> of)
        {
            _of = of;
            Debug.Log("new");
        }

        [UsedImplicitly]
        public Func<TReference, float> Is(Vector4 direction) =>
            target => _of(target).ToVector3().normalized
                                 .Dot(direction.normalized);

        public Func<TReference, float> Is<TTarget>(TTarget                target,
                                                   Func<TTarget, Vector4> selector) =>
            Is(selector(target));
    }
}