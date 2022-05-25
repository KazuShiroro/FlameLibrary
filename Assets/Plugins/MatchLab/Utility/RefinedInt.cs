using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MatchLab.Utility
{
    [CreateAssetMenu(menuName = "MatchLabLib/Utility/RefinedInt")]
    public class RefinedInt : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField]
        protected int min;

        [SerializeField]
        protected int max;

        [SerializeField]
        protected int initial;

        [SerializeField]
        protected int value;

        public int Min { get { return min; } }

        public int Max { get { return max; } }

        public int Initial { get { return initial; } }

        public int Value { get { return value; } }

        public void Clamp() => value = Mathf.Clamp(Value, Min, Max);

        public void Increment(int v) => OnIncrease.Invoke(v);

        public void Decrement(int v) => OnDecrease.Invoke(v);

        public void Change(int v) { value = v; Clamp(); OnChanged?.Invoke(); }

        public float Normalized => value / (min + max);

        public bool IsMax() => Value >= Max;

        public bool IsMin() => Value <= Min;

        public UnityAction<int> OnIncrease;

        public UnityAction<int> OnDecrease;

        public UnityAction OnMax;

        public UnityAction OnMin;

        public UnityAction OnChanged;

        public RefinedInt()
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
