using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Flame.UI;
using Flame.UI.Modal;
using Ex.Unity2019.UI;

namespace Ex.Unity2019
{
    public class StageSelect : ScreenBase
    {
        [SerializeField]
        private Button _quitButton;

        [SerializeField]
        private Button _startButton;

        private void Awake()
        {
            ModalContainer
                .Find(UIResourceName.ModalContainer)
                .PreLoad(UIResourceName.IsQuitModal);

            _quitButton.onClick.AddListener(QuitButton);

            _startButton.onClick.AddListener(StartButton);
        }

        private void StartButton()
        {
            TestSceneNavigator.Instance.LoadScene<TestHome>();
        }

        private void QuitButton()
        {
            ModalContainer
                .Find(UIResourceName.ModalContainer)
                .Push(UIResourceName.IsQuitModal);
        }
    }

}
