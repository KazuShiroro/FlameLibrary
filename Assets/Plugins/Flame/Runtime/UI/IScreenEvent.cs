using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flame.UI
{
    public interface IScreenEvent
    {
        void Pop();

        void Push(string screenName, bool addLoad = false);

        void Push(ScreenBase screenBase);
    }
}
