using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using Cysharp.Threading.Tasks;

namespace Flame.UI.Page
{
    public class PageContainer : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("キーとなるユニーク(一意)な名前")]
        protected string _uniqueName;

        /// <summary> コンテナのキャッシュ : それ自身の名前がキー </summary>
        protected static Dictionary<string, PageContainer> _containerCacheByName = new Dictionary<string, PageContainer>();

        /// <summary> コンテナのキャッシュ:ページのTransformがキー </summary>
        protected static Dictionary<int, PageContainer> _containerCacheByTransform = new Dictionary<int, PageContainer>();

        /// <summary> ページのインスタンスへの辞書 </summary>
        protected Dictionary<string, ScreenBase> _pageByName = new Dictionary<string, ScreenBase>();

        /// <summary> ページの履歴 </summary>
        protected Stack<ScreenBase> _pageLog = new Stack<ScreenBase>();

        private void Awake()
        {
            if(!string.IsNullOrWhiteSpace(_uniqueName))
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
                // 自身と同じPageContainerを抽出
                if(Equals(cache.Value))
                {
                    keysToRemove.Add(cache.Key);
                }
            }
            foreach (var keyToRemove in keysToRemove)
            {
                _containerCacheByTransform.Remove(keyToRemove);
            }
        }

        public static PageContainer Of(Transform transform)
        {
            return Of((RectTransform)transform);
        }

        /// <summary>
        /// rectTransfromが所属しているPageContainer取得する
        /// </summary>
        /// <param name="rectTransform"></param>
        /// <returns>PageContainer</returns>
        public static PageContainer Of(RectTransform rectTransform)
        {
            // 呼び出し元のオブジェクトのインスタンスIDをコンテナのキーとする
            var id = rectTransform.GetInstanceID();

            if(_containerCacheByTransform.TryGetValue(id, out var container))
            {
                return container;
            }

            // 存在しなかった場合コンテナのDictionaryに追加する
            container = rectTransform.GetComponentInParent<PageContainer>();
            if(container != null)
            {
                _containerCacheByTransform.Add(id, container);
                return container;
            }

            return null;
        }

        /// <summary>
        /// containerNameと同じ_uniqueNameのPageContainerを取得 : 
        /// 自分が所属していないPageContainerも取得可
        /// </summary>
        /// <param name="containerName"></param>
        /// <returns>PageContainer</returns>
        public static PageContainer Find(string containerName)
        {
            if(_containerCacheByName.TryGetValue(containerName,out var instance))
            {
                return instance;
            }

            return null;
        }

        public ButtonActionRegister PreLoad(string assetName)
        {
            if(_pageByName.TryGetValue(assetName,out var page))
            {
                // すでにロードされている
                return new ButtonActionRegister(() => Push(assetName));
            }

            var prefab = 
                Addressables
                .LoadAssetAsync<GameObject>(assetName)
                .WaitForCompletion();

            // UIをコンテナの子として生成
            var instance = Instantiate(prefab, transform);
            instance.name = assetName;
            // 辞書に登録
            _pageByName.Add(assetName, instance.GetComponent<ScreenBase>());
            instance.SetActive(false);

            Addressables.Release(prefab);

            return new ButtonActionRegister(() => Push(assetName));
        }

        public void Push(string assetName)
        {
            Push(_pageByName[assetName]);
        }

        public void Push(ScreenBase screenBase)
        {
            var exitPage = _pageLog.Count > 0 ? _pageLog?.Peek() : null;
            exitPage?.Off().Forget();

            _pageLog.Push(screenBase);
            var enterPage = _pageLog.Peek();
            enterPage?.On().Forget();
        }

        public void Pop()
        {
            var exitPage = _pageLog.Count > 0 ? _pageLog?.Pop() : null;
            exitPage?.Off().Forget();

            var enterPage = _pageLog.Count > 0 ? _pageLog?.Peek() : null;
            enterPage?.On().Forget();
        }
    }
}
