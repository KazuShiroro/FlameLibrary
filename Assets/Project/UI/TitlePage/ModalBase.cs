using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Flame.UI;
using Flame.UI.Page;
using Flame.UI.Modal;
using Ex.Unity2019.UI;

namespace Ex.Unity2019
{
    public class ModalBase : ScreenBase
    {
        [SerializeField]
        private Button _button_0;

        [SerializeField]
        private Button _button_1;

        private void Awake()
        {
            _button_0.onClick.AddListener(() => ModalContainer.Of(transform).Pop());

            _button_1.onClick.AddListener(() =>
            {
                ModalContainer.Of(transform).Pop(isAnimation: false);
                PageContainer.Find(UIResourceName.MainPageContainer).Pop();
            });
        }
    }
}
