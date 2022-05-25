using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MatchLab.UI
{
    public interface IScreenEvent
    {
        void Pop();

        void Push(string screenName, bool addLoad = false);

        void Push(ScreenBase screenBase);
    }
}
