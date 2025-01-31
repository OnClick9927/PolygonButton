﻿using UnityEngine;
using UnityEngine.UI;


namespace UIExtensions
{
    [AddComponentMenu("UI/Extensions/CircleRawImage")]
    public class CircleRawImage : RawImage, ICanvasRaycastFilter
    {
        public bool useIrregularRaycast = false;

        [SerializeField]
        private float radius;

        public float Radius
        {
            get => radius;
            set
            {
                var size = rectTransform.sizeDelta;
                float max = size.x > size.y ? size.x / 2f : size.y / 2f;
                radius = Mathf.Clamp(value, 0f, max);
            }
        }


        public bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
        {
            if (!useIrregularRaycast)
                return RectTransformUtility.RectangleContainsScreenPoint(rectTransform, screenPoint, eventCamera);

            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, screenPoint, eventCamera, out var pos);
            return Overlap(pos);
        }

        public bool Overlap(Vector2 target)
        {
            return Util.CircleOverlap(Vector2.zero, radius, target);
        }
    }
}