using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Flame.Game;
using UniRx;

namespace Flame.Unity
{
    /// <summary>
    /// Animatorクラス拡張メソッド
    /// </summary>
    public static class AnimatorExtentions
    {
        /// <summary>
        /// ポーズ時の処理追加
        /// </summary>
        /// <param name="animator"> self </param>
        /// <param name="gameObject"> self object </param>
        public static void SetupOnPause(this Animator animator, GameObject gameObject)
        {
            // ポーズ時
            GameStateBase.Instance.OnPaused.Subscribe(x =>
            {
                animator.speed = 0f;
            }).AddTo(gameObject);

            // ポーズ再開時
            GameStateBase.Instance.OnResumed.Subscribe(x =>
            {
                animator.speed = 1f;
            }).AddTo(gameObject);
        }
    }
}
