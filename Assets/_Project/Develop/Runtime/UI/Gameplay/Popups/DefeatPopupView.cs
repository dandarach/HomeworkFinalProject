using System;
using UnityEngine;
using UnityEngine.UI;
using Assets._Project.Develop.Runtime.UI.Core;
using TMPro;
using DG.Tweening;

namespace Assets._Project.Develop.Runtime.UI.Gameplay.Popups
{
    public class DefeatPopupView : PopupViewBase
    {
        public event Action ButtonClicked;

        [SerializeField] private TMP_Text _title;
        [SerializeField] private TMP_Text _message;
        [SerializeField] private Button _closeButton;

        public void SetTitle(string title) => _title.text = title;
        public void SetMessageText(string title) => _message.text = title;
        protected override void OnPreShow()
        {
            base.OnPreShow();

            _closeButton.onClick.AddListener(OnButtonClciked);
        }

        protected override void OnPreHide()
        {
            base.OnPreHide();

            _closeButton.onClick.RemoveListener(OnButtonClciked);
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(OnButtonClciked);
        }

        private void OnButtonClciked() => ButtonClicked?.Invoke();

        protected override void ModifyShowAnimation(Sequence animation)
        {
            base.ModifyShowAnimation(animation);

            animation.Append(_closeButton.transform
                .DOScale(Vector3.one, 0.5f)
                .From(Vector3.zero)
                .SetEase(Ease.OutBack));
            animation.AppendInterval(0.1f);
        }
    }
}
