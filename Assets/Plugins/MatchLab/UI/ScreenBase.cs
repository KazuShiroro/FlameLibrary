using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cysharp.Threading.Tasks;
using MatchLab.Animation;

namespace MatchLab.UI
{
    public class ScreenBase : MonoBehaviour
    {
        [SerializeField]
        protected ScreenInfo _screenInfo;

        public virtual async UniTask On()
        {
            gameObject.SetActive(true);
            transform.SetAsLastSibling();

            await TaskOnTransition(_screenInfo.EnterAnimation);
        }

        public virtual async UniTask Off()
        {
            await TaskOnTransition(_screenInfo.ExitAnimation);

            gameObject.SetActive(false);
        }

        protected virtual async UniTask TaskOnTransition(TransformAnimation transformAnimation)
        {
            const float timeSpan = 0.01f;
            float seconds = 0f;

            while (seconds < transformAnimation?.Time)
            {
                transformAnimation?.Animation(transform, seconds += timeSpan);
                await UniTask.Delay(TimeSpan.FromSeconds(timeSpan));
            }
        }
    }
}
