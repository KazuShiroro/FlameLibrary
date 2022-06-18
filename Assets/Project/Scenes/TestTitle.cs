using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Flame.UI;
using Flame.UI.Page;
using Flame.Audio.BGM;
using Ex.Unity2019.UI;
using Ex.Unity2019.AAS.Audio;

namespace Ex.Unity2019
{
    public class TestTitle : TestSceneBase
    {
        protected override void OnStart()
        {
            PageContainer
                .Find(UIResourceName.MainPageContainer)
                .PreLoad(UIResourceName.TitlePage)
                .Push();

            BGMManager.Instance.Play(AddressableName_BGM.BGM_CYBER_17);
        }

        private void OnDisable()
        {
            //BGMManager.Instance.Stop();
        }

        public void GoHome() => TestSceneNavigator.Instance?.LoadScene<TestHome>();
    }
}
