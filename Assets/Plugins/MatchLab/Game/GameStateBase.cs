using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MatchLab.Singleton;
using UniRx;

namespace MatchLab.Game
{
    public class GameStateBase : SingletonMonoBehaviour<GameStateBase>
    {
        protected Subject<bool> pauseSbject = new Subject<bool>();

        protected Subject<bool> resumuSbject = new Subject<bool>();

        protected bool isPausing = false;

        public bool IsPausing() { return isPausing; }

        public IObservable<bool> OnPaused
        {
            get { return pauseSbject; }
        }

        public IObservable<bool> OnResumed
        {
            get { return resumuSbject; }
        }
        
        public void Pause()
        {
            isPausing = true;

            pauseSbject.OnNext(isPausing);
        }

        public void Resume()
        {
            isPausing = false;

            resumuSbject.OnNext(isPausing);
        }
    }
}
