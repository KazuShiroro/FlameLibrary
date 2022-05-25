using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using Cysharp.Threading.Tasks;

namespace MatchLab.UI
{
    public class TestSceneNavigator : MonoBehaviour
    {
        public GameObject loadinBarrier;

        private static TestSceneNavigator instance;

        public static TestSceneNavigator Instance
        {
            get
            {
                if (instance == null)
                {
                    var prefab = Addressables.LoadAssetAsync<GameObject>(typeof(TestSceneNavigator).Name).WaitForCompletion();

                    var go = Instantiate(prefab);
                    Addressables.Release(prefab);

                    instance = go.GetComponent<TestSceneNavigator>();
                    DontDestroyOnLoad(instance.gameObject);
                }

                return instance;
            }
        }

        public Action OnLoadScceneEnter;

        public Action OnLoadScceneExit;

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
                DontDestroyOnLoad(instance.gameObject);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        public void LoadScene<T>(object options = null)
            where T : TestSceneBase
        {
            LoadSceneAsync<T>(options).Forget();
        }

        public async UniTask LoadSceneAsync<T> (object options = null)
            where T : TestSceneBase
        {
            OnLoadScceneEnter?.Invoke();
            loadinBarrier.SetActive(true);

            await SceneManager.LoadSceneAsync(typeof(T).Name);

            OnLoadScceneExit?.Invoke();
            loadinBarrier.SetActive(false);

            var nextScene = FindObjectOfType<T>();

            if(nextScene == null)
            {
                throw new System.Exception(typeof(T).Name + "is Null");
            }

            nextScene.OnLoad(options);
        }
    }
}
