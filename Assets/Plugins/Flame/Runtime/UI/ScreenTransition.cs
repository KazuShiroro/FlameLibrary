using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Flame.Animation;

namespace Flame.UI
{
    [Serializable]
    public class ScreenTransition
    {
        [SerializeField]
        protected Button _button;
        public Button Button { get => _button; }

        [SerializeField]
        protected GameObject _screen;
        public GameObject Screen { get => _screen; }

    }
}
