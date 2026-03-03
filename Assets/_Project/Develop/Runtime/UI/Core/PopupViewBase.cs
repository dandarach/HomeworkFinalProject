using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.UI.Core
{
    public abstract class PopupViewBase : MonoBehaviour, IShowableView
    {
        public event Action CloseRequest;

        [SerializeField] private CanvasGroup _mainGroup;

        private void Awake()
        {
            _mainGroup.alpha = 0;
        }

        public void OnCloseButtonClicked() => CloseRequest?.Invoke();

        public void Show()
        {
            OnPreShow();

            // TODO: animation
            _mainGroup.alpha = 1;

            OnPostShow();
        }

        public void Hide()
        {
            OnPreHide();

            // TODO: animation
            _mainGroup.alpha = 0;

            OnPostHide();
        }

        protected virtual void OnPreShow() { }

        protected virtual void OnPostShow() { }

        protected virtual void OnPreHide() { }

        protected virtual void OnPostHide() { }
    }
}
