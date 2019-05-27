using System;
using FluentMotion.detectors;
using UnityEngine;

namespace Demo
{
    public readonly struct Expectation
    {
        public GestureType Gesture { get; }
        public Material Material { get; }

        public Expectation(Material material)
        {
            Material = material;
            Gesture = GetTypeForMaterial(Material.name);
        }

        private static GestureType GetTypeForMaterial(string material)
        {
            switch (material)
            {
                case "fist":
                    return GestureType.Fist;
                case "pinch":
                    return GestureType.Pinch;
                case "swipe-down":
                    return GestureType.SwipeDown;
                case "swipe-left":
                    return GestureType.SwipeLeft;
                case "swipe-right":
                    return GestureType.SwipeRight;
                case "swipe-up":
                    return GestureType.SwipeUp;
                case "thumb-index-extended":
                    return GestureType.ThumbIndexExtended;
                case "thumbs-up":
                    return GestureType.ThumbsUp;
                default:
                    return GestureType.None;
            }
        }
    }

    public enum GestureType
    {
        Fist,
        Pinch,
        SwipeDown,
        SwipeUp,
        SwipeLeft,
        SwipeRight,
        ThumbIndexExtended,
        ThumbsUp,
        None
    }
}