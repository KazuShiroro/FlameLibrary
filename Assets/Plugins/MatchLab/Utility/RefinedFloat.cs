using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MatchLab.Utility
{
    [CreateAssetMenu(menuName = "MatchLabLib/Utility/RefinedFloat")]
    public class RefinedFloat : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField]
        protected float min;

        [SerializeField]
        protected float max;

        [SerializeField]
        protected float initial;

        [SerializeField]
        protected float value;

        public float Min { get { return min; } }

        public float Max { get { return max; } }

        public float Initial { get { return initial; } }

        public float Value { get { return value; } }

        public void Clamp() => value = Mathf.Clamp(Value, Min, Max);

        public void Increment(float v) => OnIncrease.Invoke(v);

        public void Decrement(float v) => OnDecrease.Invoke(v);

        public void Change(float v) { value = v; Clamp(); OnChanged?.Invoke(); }

        public float Normalized => value / (min + max);

        public bool IsMax() => Value >= Max;

        public bool IsMin() => Value <= Min;

        public UnityAction<float> OnIncrease;

        public UnityAction<float> OnDecrease;

        public UnityAction OnMax;

        public UnityAction OnMin;

        public UnityAction OnChanged;

        public RefinedFloat()
        {
            OnIncrease += v => Change((value += v));

            OnDecrease += v => Change((value -= v));

            OnChanged += () =>
            {
                if (IsMax())
                {
                    OnMax?.Invoke();
                }
                else if (IsMin())
                {
                    OnMin?.Invoke();
                }
            };
        }

        public void OnAfterDeserialize()
        {
            min = Mathf.Min(Min, Max);

            initial = Mathf.Clamp(Initial, Min, Max);

            Change(Initial);
        }

        public void OnBeforeSerialize()
        {
        }
    }
}
