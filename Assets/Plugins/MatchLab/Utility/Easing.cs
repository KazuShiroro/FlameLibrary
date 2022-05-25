using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchLab.Utility
{
    public enum EaseType
    {
        Linear,

        InSine,
        OutSine,
        InOutSine,

        InQuad,
        OutQuad,
        InOutQuad,

        InCubic,
        OutCubic,
        InOutCubic,

        InQuart,
        OutQuart,
        InOutQuart,

        InQuint,
        OutQuint,
        InOutQuint,

        InExpo,
        OutExpo,
        InOutExpo,

        InCirc,
        OutCirc,
        InOutCirc,

        InBack,
        OutBack,
        InOutBack,

        //InElastic,
        //OutElastic,
        //InOutElastic,

        InBounce,
        OutBounce,
        InOutBounce,
    }

    public static class Easing
    {
        private static float PI => Mathf.PI;

        public static float rad90 => Mathf.Deg2Rad * 90f;

        public static float Ease(EaseType easeType, float time,float finishTime, float start, float end)
        {
            float v = 0;

            switch (easeType)
            {
                case EaseType.Linear: v = Linear(time, finishTime, start, end); break;

                case EaseType.InSine: v = InSine(time, finishTime, start, end); break;
                case EaseType.OutSine: v = OutSine(time, finishTime, start, end); break;
                case EaseType.InOutSine: v = InOutSine(time, finishTime, start, end); break;

                case EaseType.InQuad: v = InQuad(time, finishTime, start, end); break;
                case EaseType.OutQuad: v = OutQuad(time, finishTime, start, end); break;
                case EaseType.InOutQuad: v = InOutQuad(time, finishTime, start, end); break;

                case EaseType.InCubic: v = InCubic(time, finishTime, start, end); break;
                case EaseType.OutCubic: v = OutCubic(time, finishTime, start, end); break;
                case EaseType.InOutCubic: v = InOutCubic(time, finishTime, start, end); break;

                case EaseType.InQuart: v = InQuart(time, finishTime, start, end); break;
                case EaseType.OutQuart: v = OutQuart(time, finishTime, start, end); break;
                case EaseType.InOutQuart: v = InOutQuart(time, finishTime, start, end); break;

                case EaseType.InQuint: v = InQuint(time, finishTime, start, end); break;
                case EaseType.OutQuint: v = OutQuint(time, finishTime, start, end); break;
                case EaseType.InOutQuint: v = InOutQuint(time, finishTime, start, end); break;

                case EaseType.InExpo: v = InExpo(time, finishTime, start, end); break;
                case EaseType.OutExpo: v = OutExpo(time, finishTime, start, end); break;
                case EaseType.InOutExpo: v = InOutExpo(time, finishTime, start, end); break;

                case EaseType.InCirc: v = InCirc(time, finishTime, start, end); break;
                case EaseType.OutCirc: v = OutCirc(time, finishTime, start, end); break;
                case EaseType.InOutCirc: v = InOutCirc(time, finishTime, start, end); break;

                case EaseType.InBack: v = InBack(time, finishTime, start, end); break;
                case EaseType.OutBack: v = OutBack(time, finishTime, start, end); break;
                case EaseType.InOutBack: v = InOutBack(time, finishTime, start, end); break;

                //case EaseType.InElastic: v = InElastic(time, finishTime, start, end); break;
                //case EaseType.OutElastic: v =  OutElastic(time, finishTime, start, end); break;
                //case EaseType.InOutElastic: v = InOutElastic(time, finishTime, start, end); break;

                case EaseType.InBounce: v = InBounce(time, finishTime, start, end); break;
                case EaseType.OutBounce: v = OutBounce(time, finishTime, start, end); break;
                case EaseType.InOutBounce: v = InOutBounce(time, finishTime, start, end); break;
            }

            return v;
        }

        #region Linear
        public static float Linear(float t, float f, float s, float e)
        {
            return (s - e) * t / f;
        }
        #endregion

        #region Sine
        public static float InSine(float t, float f, float s, float e)
        {
            e -= s;
            return -e * Mathf.Cos(t * rad90 / f) + e + s;
        }

        public static float OutSine(float t, float f, float s, float e)
        {
            e -= s;
            return e * Mathf.Sin(t * rad90 / f) + s;
        }

        public static float InOutSine(float t, float f, float s, float e)
        {
            e -= s;
            return -e / 2f * (Mathf.Sin(t * Mathf.Deg2Rad * 90f / f) - 1f) + s;
        }
        #endregion

        #region Quad
        public static float InQuad(float t, float f, float s, float e)
        {
            e -= s;
            t /= f;
            return e * t * t + s;
        }

        public static float OutQuad(float t, float f, float s, float e)
        {
            e -= s;
            t /= f;
            return -e * t * (t - 2f) + s;
        }

        public static float InOutQuad(float t, float f, float s, float e)
        {
            e -= s;
            t /= f / 2f;

            if (t < 1f)
                return e / 2f * t * t + s;

            t -= 1f;

            return -e / 2f * (t * (t - 2f) - 1f) + s;
        }
        #endregion

        #region Cubic
        public static float InCubic(float t, float f, float s, float e)
        {
            e -= s;
            t /= f;
            return e * t * t * t + s;
        }

        public static float OutCubic(float t, float f, float s, float e)
        {
            e -= s;
            t /= t / f - 1f;
            return e * (t * t * t + 1f) + s;
        }

        public static float InOutCubic(float t, float f, float s, float e)
        {
            e -= s;
            t /= f / 2f;

            if (t < 1f)
                return e / 2f * t * t * t + s;

            t -= 2f;

            return e / 2f * (t * t * t + 2f) + s;
        }
        #endregion

        #region Quart
        public static float InQuart(float t, float f, float s, float e)
        {
            e -= s;
            t /= f;
            return e * t * t * t * t + s;
        }

        public static float OutQuart(float t, float f, float s, float e)
        {
            e -= s;
            t /= t / f - 1f;
            return -e * (t * t * t * t - 1f) + s;
        }

        public static float InOutQuart(float t, float f, float s, float e)
        {
            e -= s;
            t /= f / 2f;

            if (t < 1f)
                return e / 2f * t * t * t * t + s;

            t -= 2f;

            return -e / 2f * (t * t * t * t - 2f) + s;
        }
        #endregion

        #region Quint
        public static float InQuint(float t, float f, float s, float e)
        {
            e -= s;
            t /= f;
            return e * t * t * t * t * t + s;
        }

        public static float OutQuint(float t, float f, float s, float e)
        {
            e -= s;
            t = t / f - 1f;
            return e * (t * t * t * t * t + 1f) + s;
        }

        public static float InOutQuint(float t, float f, float s, float e)
        {
            e -= s;
            t /= f / 2f;

            if (t < 1f)
                return e / 2f * t * t * t * t * t + s;

            t -= 2f;

            return e / 2f * (t * t * t * t * t + 2f) + s;
        }
        #endregion

        #region Expo
        public static float InExpo(float t, float f, float s, float e)
        {
            e -= s;
            return t == 0f ? s : e * Mathf.Pow(2f, 10f * (t / e - 1f)) + s;
        }

        public static float OutExpo(float t, float f, float s, float e)
        {
            e -= s;
            return t == f ? e + s : e * (-Mathf.Pow(2f, -10f * t / f) + 1) + s;
        }

        public static float InOutExpo(float t, float f, float s, float e)
        {
            if (t == 0) return s;

            if (t == f) return e;

            e -= s;

            t /= f / 2f;

            if (t < 1f)
                return e / 2f * Mathf.Pow(2f, 10f * (t - 1f)) + s;

            t -= 1f;

            return e / 2f * (-Mathf.Pow(2f, -10f * t) + 2f) + s;
        }
        #endregion

        #region Circ
        public static float InCirc(float t, float f, float s, float e)
        {
            e -= s;
            t /= f;
            return -e * (Mathf.Sqrt(1f - t * t) - 1f) + s;
        }

        public static float OutCirc(float t, float f, float s, float e)
        {
            e -= s;
            t = t / f - 1f;
            return e * Mathf.Sqrt(1f - t * t) + s;
        }

        public static float InOutCirc(float t, float f, float s, float e)
        {
            e -= s;

            t /= f / 2f;

            if (t < 1f)
                return -e / 2f * (Mathf.Sqrt(1f - t * t) - 1f) + s;

            t -= 2f;

            return e / 2f * (Mathf.Sqrt(1f - t * t) + 1f) + s;
        }
        #endregion

        #region Back
        public static float InBack(float t, float f, float s, float e, float c = 1f)
        {
            e -= s;
            t /= f;
            return e * t * t * ((c + 1f) * t - c) + s;
        }

        public static float OutBack(float t, float f, float s, float e, float c = 1f)
        {
            e -= s;
            t = t / f - 1f;
            return e * (t * t * ((c + 1f) * t + c) + 1f) + s;
        }

        public static float InOutBack(float t, float f, float s, float e, float c = 1f)
        {
            e -= s;

            c *= 1.525f;

            t /= f / 2f;

            if (t < 1f)
                return e / 2f * (t * t * ((c + 1f) * t - c)) + s;

            t -= 2f;

            return e /2f  * (t * t * ((c + 1f) * t + c) + 2f) + s;
        }
        #endregion

        //#region Elastic
        //public static float InElastic(float t, float f, float s, float e)
        //{
        //    e -= s;
        //    t /= f;
        //    return e * t * t * ((c + 1f) * t - c) + s;
        //}

        //public static float OutElastic(float t, float f, float s, float e)
        //{
        //    e -= s;
        //    t = t / f - 1f;
        //    return e * (t * t * ((c + 1f) * t + c) + 1f) + s;
        //}

        //public static float InOutElastic(float t, float f, float s, float e)
        //{
        //    e -= s;

        //    c *= 1.525f;

        //    t /= f / 2f;

        //    if (t < 1f)
        //        return e / 2f * (t * t * ((c + 1f) * t - c)) + s;

        //    t -= 2f;

        //    return e /2f  * (t * t * ((c + 1f) * t + c) + 2f) + s;
        //}
        //#endregion

        #region Bounce
        public static float InBounce(float t, float f, float s, float e)
        {
            e -= s;
            t /= f;
            return e * Mathf.Pow(2f, 6f * (t - 1f)) * Mathf.Abs(Mathf.Sin(t * PI * 3.5f)) + s;
        }

        public static float OutBounce(float t, float f, float s, float e)
        {
            e -= s;
            t /= f;

            if(t < 1f / 2.75f)
            {
                return e * (7.5625f * t * t) + s;
            }
            else if(t < 2f / 2.75f)
            {
                t -= 1.5f / 2.75f;
                return e * (7.5625f * t * t + 0.75f) + s;
            }
            else if(t < 2.5f / 2.75f)
            {
                t -= 2.25f / 2.75f;
                return e * (7.5625f * t * t + 0.9375f) + s;
            }
            else
            {
                t -= 2.625f / 2.75f;
                return e * (7.5625f * t * t + 0.984375f) + s;
            }
        }

        public static float InOutBounce(float t, float f, float s, float e)
        {
            e -= s;

            t /= f;

            if (t < 0.5f)
                return e * 8.0f * Mathf.Pow(2.0f, 8.0f * (t - 1.0f)) * Mathf.Abs(Mathf.Sin(t * PI * 7.0f)) + s;
            else
                return e * (1.0f - 8.0f * Mathf.Pow(2.0f, -8.0f * t) * Mathf.Abs(Mathf.Sin(t * PI * 7.0f))) + s;
        }
        #endregion
    }
}
