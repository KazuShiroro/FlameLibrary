using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flame.UI
{
    #region CanvasInfo
    [Serializable]
    public class CanvasReference
    {
        [SerializeField]
        private CanvasID id;
        public CanvasID ID { get => id; }

        [SerializeField]
        private ScreenContainer _screenContainer;
        public ScreenContainer ScreenContainer { get => _screenContainer; }
    }
    #endregion

    public class CanvasContainer : MonoBehaviour
    {
        [SerializeField]
        protected List<CanvasReference> _screenReferences = new List<CanvasReference>();

        public ScreenContainer Of(CanvasID canvasID)
        {
            return _screenReferences.Find(canvas => canvas.ID == canvasID).ScreenContainer;
        }
    }
}
