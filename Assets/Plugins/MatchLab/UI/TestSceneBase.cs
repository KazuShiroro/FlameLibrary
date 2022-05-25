using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchLab.UI
{
    public abstract class TestSceneBase : MonoBehaviour
    {
        private void Awake()
        {
            OnAwake();
        }

        private void Start()
        {
            OnStart();
        }

        protected virtual void OnAwake() { }

        protected virtual void OnStart() { }

        public virtual void OnLoad(object options = null) { }
    }   
}
