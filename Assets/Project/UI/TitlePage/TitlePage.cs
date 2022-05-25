using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MatchLab.UI;
using MatchLab.UI.Page;
using MatchLab.Audio.SE;
using Ex.Unity2019.UI;
using Ex.Unity2019.AAS.Audio;

namespace Ex.Unity2019
{
    public class TitlePage : ScreenBase
    {
        [SerializeField]
        private Button _startButton;

        private void Start()
        {
            PageContainer
                .Find(UIResourceName.MainPageContainer)
                .PreLoad(UIResourceName.StageSelect);

            _startButton.onClick.AddListener(StartButon);
        }

        private void StartButon()
        {
            PageContainer
                .Find(UIResourceName.MainPageContainer)
                .Push(UIResourceName.StageSelect);
        }
    }
}
