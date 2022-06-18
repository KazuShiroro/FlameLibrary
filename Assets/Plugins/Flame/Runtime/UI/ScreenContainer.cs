using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Flame.UI
{
    public abstract class  ScreenContainer : MonoBehaviour
    {
        public abstract void Pop();

        public abstract ButtonActionRegister PreLoad(string screenName);

        public abstract void Push(ScreenBase screenBase);
    }
}
