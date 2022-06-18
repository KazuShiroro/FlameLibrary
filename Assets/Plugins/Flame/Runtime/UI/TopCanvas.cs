using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flame.UI
{
    public class TopCanvas : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
