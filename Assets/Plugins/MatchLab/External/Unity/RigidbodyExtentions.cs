using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Flame.Game;
using UniRx;

namespace Flame.Unity
{
    /// <summary>
    /// ポーズ前のRigidbodyのデータをキャッシュるクラス
    /// </summary>
    public class CacheRigidbodyOnPause : MonoBehaviour
    {
        public Vector3 velocity;

        public Vector3 angularVelocity;

        public void SetCahceRigidbodyOnPause(Rigidbody rigidbody)
        {
            this.velocity = rigidbody.velocity;

            this.angularVelocity = rigidbody.angularVelocity;
        }
    }

    /// <summary>
    /// Rigidbodyクラス拡張メソッド
    /// </summary>
    public static class RigidbodyExtentions
    {
        /// <summary>
        /// ポーズ時の処理追加
        /// </summary>
        /// <param name="animator"> self </param>
        public static void SetupOnPause(this Rigidbody rigidbody, GameObject gameObject)
        {
            // ポーズ時
            GameStateBase.Instance.OnPaused.Subscribe(x =>
            {
                OnPause(rigidbody, gameObject);
            });

            // ポーズ再開時
            GameStateBase.Instance.OnResumed.Subscribe(x =>
            {
                OnResume(rigidbody, gameObject);
            });
        }

        /// <summary>
        /// ポーズ時
        /// </summary>
        /// <param name="rigidbody"></param>
        /// <param name="gameObject"></param>
        public static void OnPause(this Rigidbody rigidbody, GameObject gameObject)
        {
            // Rigidbodyキャッシュ用クラスの探索
            var cache = gameObject.GetComponent<CacheRigidbodyOnPause>();

            if (!cache)
            {
                // ポーズ前のRigidbodyキャッシュ用クラスの追加
                cache = gameObject.AddComponent<CacheRigidbodyOnPause>();

                // ポーズ前のRigidbodyのデータをキャッシュ
                cache.SetCahceRigidbodyOnPause(rigidbody);
            }

            // 停止
            rigidbody.isKinematic = true;
        }

        /// <summary>
        /// ポーズ再開時
        /// </summary>
        /// <param name="rigidbody"></param>
        /// <param name="gameObject"></param>
        public static void OnResume(this Rigidbody rigidbody, GameObject gameObject)
        {
            var cache = gameObject.GetComponent<CacheRigidbodyOnPause>();

            if (!cache) return;

            // 取得
            rigidbody.velocity = cache.velocity;

            // 取得
            rigidbody.angularVelocity = cache.angularVelocity;

            // 再開
            rigidbody.isKinematic = false;
        }
    }
}
