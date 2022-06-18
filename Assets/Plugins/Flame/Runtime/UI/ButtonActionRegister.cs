using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Flame.UI
{
    public class ButtonActionRegister 
    {
        public ButtonActionRegister( Action action)
        {
            _action = action;
        }

        protected Action _action;
        protected Action Action { get => _action; }

        public void RegisterPushButton(Button button)
        {
            button.onClick.AddListener(Action.Invoke);
        }

        public void Push()
        {
            Action.Invoke();
        }
    }
}
