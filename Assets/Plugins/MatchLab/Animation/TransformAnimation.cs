using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MatchLab.Utility;

namespace MatchLab.Animation
{
    [CreateAssetMenu(menuName = ("MatchLabLib/Animation/TransformAnimation"))]
    public class TransformAnimation : ScriptableObject
    {
        [SerializeField]
        protected EaseType easeType;

        [SerializeField]
        protected float deley = 0f;

        [SerializeField]
        protected float time = 0.5f;

        public float Time { get { return time; } }

        [Header("Position")]
        [SerializeField]
        protected Vector3 startPos;

        [SerializeField]
        protected Vector3 endPos;

        [Header("Scale")]
        [SerializeField]
        protected Vector3 startScale = Vector3.one;

        [SerializeField]
        protected Vector3 endScale = Vector3.one;

        [Header("Rotation")]
        [SerializeField]
        protected Vector3 startRotation;

        [SerializeField]
        protected Vector3 endRotation;

        [Header("Alpha")]
        [SerializeField]
        protected float startAlpha = 1f;

        [SerializeField]
        protected float endAlpha = 1f;

        public EaseType EaseType { get { return easeType; } }

        public void Animation(Transform transform, float progressTime)
        {
            Animation((RectTransform)transform, progressTime);
        }

        public void Animation(RectTransform rectTransform, float progressTime)
        {
            Vector3 newScale = Vector3.one;
            newScale.x = Easing.Ease(EaseType, progressTime, time, startScale.x, endScale.x);
            newScale.y = Easing.Ease(EaseType, progressTime, time, startScale.y, endScale.y);
            newScale.z = Easing.Ease(EaseType, progressTime, time, startScale.z, endScale.z);

            rectTransform.localScale = newScale;

            float alpha = Easing.Ease(EaseType, progressTime, time, startAlpha, endAlpha);
        }
    }
}
