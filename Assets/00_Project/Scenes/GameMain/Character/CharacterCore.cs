using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using MatchLab.UniRx;

namespace Project.Character
{
    public class CharacterCore : MonoBehaviour
    {
        #region Initialize
        public IObservable<Unit> OnInitializeAsync { get { return onInitializeAsync.asyncSubject; } }
        private readonly WrappedAsyncSubject onInitializeAsync = new WrappedAsyncSubject();
        virtual public void Initialize(Vector3 spawnPoint)
        {
            onInitializeAsync.Done();
        }
        #endregion
    }
}
