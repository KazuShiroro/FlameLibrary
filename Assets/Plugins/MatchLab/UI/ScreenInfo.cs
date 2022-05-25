using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MatchLab.Animation;

namespace MatchLab.UI
{
    [Serializable]
    public class ScreenInfo
    {
        [SerializeField]
        protected TransformAnimation enterAnimation;    

        [SerializeField]
        protected TransformAnimation exitAnimation;

        public TransformAnimation EnterAnimation { get { return enterAnimation; } }

        public TransformAnimation ExitAnimation { get { return exitAnimation; } }
    }
}
