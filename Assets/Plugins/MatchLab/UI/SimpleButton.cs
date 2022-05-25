using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using MatchLab.Audio.SE;

namespace MatchLab.UI
{
    public class SimpleButton : Button
    {
        [SerializeField]
        protected AssetLabelReference _seName;

        protected override void Start()
        {
            onClick.AddListener(()=>SEManager.Instance.Play(_seName.labelString));
        }
    }
}
