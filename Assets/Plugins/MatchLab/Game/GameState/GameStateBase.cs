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
        protected Subject<string> pauseSbject = new Subject<string>();

        protected Subject<string> resumuSbject = new Subject<string>();

        protected bool isPausing = false;

        public bool IsPausing() { return isPausing; }

        public IObservable<string> OnPaused
        {
            get { return pauseSbject; }
        }

        public IObservable<string> OnResumed
        {
            get { return resumuSbject; }
        }
        
        public void Pause()
        {
            isPausing = true;

            pauseSbject.OnNext("Pause");
        }

        public void Resume()
        {
            isPausing = false;

            resumuSbject.OnNext("Resume");
        }
    }
}
