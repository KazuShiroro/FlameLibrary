using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MatchLab.UI;
using MatchLab.Audio.BGM;
using Ex.Unity2019.AAS.Audio;

namespace Ex.Unity2019
{
    public class TestHome : TestSceneBase
    {
        protected override void OnAwake()
        {
            BGMManager.Instance.Play(AddressableName_BGM.BGM_CYBER_31);
        }

        private void OnDisable()
        {
            //BGMManager.Instance.Stop();
        }

        public void GoSelectStage() => TestSceneNavigator.Instance.LoadScene<TestSelectStage>();

        public void GoTitle() => TestSceneNavigator.Instance.LoadScene<TestTitle>();
    }
}
