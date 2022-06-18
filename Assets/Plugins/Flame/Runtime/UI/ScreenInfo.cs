using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Flame.Animation;

namespace Flame.UI
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
