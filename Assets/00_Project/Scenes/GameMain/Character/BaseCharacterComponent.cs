using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.Assertions;
using Project.Character.Input;

namespace Project.Character
{
    abstract public class BaseCharacterComponent : MonoBehaviour
    {
        protected CharacterCore core;
        
        private IInputEventProvider inputEventProvider;
        protected IInputEventProvider InputEventProvider { get { return inputEventProvider; } }

        private void Start()
        {
            core = GetComponent<CharacterCore>();
            Assert.IsNotNull(core, $"{core}がありません");

            inputEventProvider = GetComponent<IInputEventProvider>();
            Assert.IsNotNull(inputEventProvider, $"{inputEventProvider}がありません");
            
            // coreの初期化終了後、初期化実行
            core.OnInitializeAsync
                .Subscribe(_ => OnInitialize());

            OnStart();
        }

        protected virtual void OnStart() { }
        protected abstract void OnInitialize();
    }

}
