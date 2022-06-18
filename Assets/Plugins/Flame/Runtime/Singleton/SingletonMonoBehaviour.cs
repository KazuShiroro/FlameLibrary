using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.AddressableAssets;
using UnityEngine.AddressableAssets;

namespace Flame.Singleton
{
    /// <summary>
    /// MonoBehaviourなシングルトンの基底クラス:AddressableAssets必須!!
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SingletonMonoBehaviour<T> : MonoBehaviour
        where T : SingletonMonoBehaviour<T>
    {
        /// <summary> シーンに配置されたインスタンスへの参照 </summary>
        protected static T _instance = null;

        /// <summary> インスタンスの取得 </summary>
        public static T Instance
        {
            get
            {
                if(_instance == null)
                {
                    // 継承先のクラス名をプレハブ名とする
                    var type = typeof(T);
                    string prefabName = type.Name;

                    // Addressablesからプレハブを読み込む
                    var loadedPrefab = Addressables
                        .LoadAssetAsync<GameObject>(prefabName)
                        .WaitForCompletion();

                    if(loadedPrefab == null)
                    {
                        Debug.LogError($"prefab:{prefabName}が存在しないか、Addressableに登録されていません。");
                        return null;
                    }

                    // シーンにインスタンス化して参照先を設定
                    _instance = Instantiate(loadedPrefab).GetComponent<T>();
                    _instance.name = prefabName;

                    // 読み込んだプレハブを解放
                    Addressables.Release(loadedPrefab);
                }

                return _instance;
            }
        }

        private void Awake()
        {
            if(_instance != null)
            {
                Destroy(gameObject);
                return;
            }
            else if(_instance == null)
            {
                _instance = (T)this;
                DontDestroyOnLoad(_instance.gameObject);
            }

            OnAwake();
        }

        private void Start()
        {
            OnStart();
        }

        /// <summary>
        /// AddComponentやResetしたときに呼ばれる
        /// </summary>
        private void Reset()
        {
            // T(ジェネリック型)からクラス情報を取得 
            var type = typeof(T);

            // クラス名(名前空間を含まない)をgameObjectの名前にセット
            gameObject.name = type.Name;
            OnReset();
        }

        protected virtual void OnAwake() { }

        protected virtual void OnStart() { }

        /// <summary>
        /// AddComponentやResetしたときに呼ばれる
        /// </summary>
        protected virtual void OnReset() { }
    }
}
