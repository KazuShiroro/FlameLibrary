using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using MatchLab.Audio.SE;

namespace MatchLab.UI
{
    public class ButtonSERegister : MonoBehaviour
    {
        [SerializeField]
        protected AssetReference _seAssetName;

        private void Start()
        {
            TryGetComponent<Button>(out var button);

            button?.onClick.AddListener(() => SEManager.Instance.Play(_seAssetName.AssetGUID));
        }
    }

}
