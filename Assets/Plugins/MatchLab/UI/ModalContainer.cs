using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

namespace Flame.UI.Modal
{
    public class ModalContainer : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("キーとなるユニーク(一意)な名前")]
        protected string _uniqueName;

        [SerializeField]
        protected ScreenBase _modalOverley;

        /// <summary> コンテナのキャッシュ : それ自身の名前がキー </summary>
        protected static Dictionary<string, ModalContainer> _containerCacheByName = new Dictionary<string, ModalContainer>();

        /// <summary> コンテナのキャッシュ:ページのTransformがキー </summary>
        protected static Dictionary<int, ModalContainer> _containerCacheByTransform = new Dictionary<int, ModalContainer>();

        protected Dictionary<string, ScreenBase> _modalByName = new Dictionary<string, ScreenBase>();

        protected Stack<ScreenBase> _modalLog = new Stack<ScreenBase>();

        private void Awake()
        {
            if (!string.IsNullOrWhiteSpace(_uniqueName))
            {
                _containerCacheByName.Add(_uniqueName, this);
            }
        }

        private void OnDestroy()
        {
            // _uniqueName対応のDictionaryからRemove
            _containerCacheByName.Remove(_uniqueName);

            // UIから呼び出されるDictionaryからRemove
            var keysToRemove = new List<int>();
            foreach (var cache in _containerCacheByTransform)
            {
                // 自身と同じModalContainerを抽出
                if (Equals(cache.Value))
                {
                    keysToRemove.Add(cache.Key);
                }
            }
            foreach (var keyToRemove in keysToRemove)
            {
                _containerCacheByTransform.Remove(keyToRemove);
            }
        }

        public static ModalContainer Of(Transform transform)
        {
            return Of((RectTransform)transform);
        }

        public static ModalContainer Of(RectTransform rectTransform)
        {
            // 呼び出し元のオブジェクトのインスタンスIDをコンテナのキーとする
            var id = rectTransform.GetInstanceID();

            if (_containerCacheByTransform.TryGetValue(id, out var container))
            {
                return container;
            }

            // 存在しなかった場合コンテナのDictionaryに追加する
            container = rectTransform.GetComponentInParent<ModalContainer>();
            if (container != null)
            {
                _containerCacheByTransform.Add(id, container);
                return container;
            }

            return null;
        }

        public static ModalContainer Find(string containerName)
        {
            if (_containerCacheByName.TryGetValue(containerName, out var instance))
            {
                return instance;
            }

            return null;
        }

        public ButtonActionRegister PreLoad(string assetName)
        {
            if (_modalByName.TryGetValue(assetName, out var modal))
            {
                // すでにロードされている
                return new ButtonActionRegister(() => Push(assetName));
            }

            var prefab = Addressables
                .LoadAssetAsync<GameObject>(assetName)
                .WaitForCompletion();

            // UIをコンテナの子として生成
            var instance = Instantiate(prefab, transform);
            instance.name = assetName;
            // 辞書に登録
            _modalByName.Add(assetName, instance.GetComponent<ScreenBase>());
            instance.gameObject.SetActive(false);

            Addressables.Release(prefab);

            return new ButtonActionRegister(() => Push(assetName));
        }

        public void Push(string assetName)
        {
            Push(_modalByName[assetName]);
        }

        public void Push(ScreenBase screenBase)
        {
            _modalOverley.On().Forget();

            _modalLog.Push(screenBase);
            var enterScreen = _modalLog.Peek();

            enterScreen.On().Forget();
        }

        public void Pop(bool isAnimation = true)
        {
            var exitScreen = _modalLog.Pop();

            if (isAnimation)
                exitScreen.Off().Forget();
            else
                exitScreen?.gameObject.SetActive(false);

            if (_modalLog.Count <= 0)
            {
                _modalOverley.Off().Forget();
            }
        }
    }
}
