using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Develop.Runtime.UI.Core
{
    public abstract class PopupViewBase : MonoBehaviour, IShowableView
    {
        public event Action CloseRequest;

        [SerializeField] private CanvasGroup _mainGroup;
        [SerializeField] private Image _anticlicker;
        [SerializeField] private Transform _body;

        private Tween _currentAnimation;

        private void Awake()
        {
            _mainGroup.alpha = 0;
        }

        public void OnCloseButtonClicked() => CloseRequest?.Invoke();

        public void Show()
        {
            KillCurrentAnimation();

            OnPreShow();

            // TODO: animation
            _mainGroup.alpha = 1;

            Sequence animation = DOTween.Sequence();

            animation
                .Append(_anticlicker
                    .DOFade(0.75f, 0.2f)
                    .From(0))
                .Join(_body
                    .DOScale(1, 0.5f)
                    .From(0)
                    .SetEase(Ease.OutBack));

            ModifyShowAnimation(animation);

            _currentAnimation = animation;

            OnPostShow();
        }

        public void Hide()
        {
            KillCurrentAnimation();

            OnPreHide();

            Sequence animation = DOTween.Sequence();
            
            ModifyHideAnimation(animation);

            _currentAnimation = animation;

            OnPostHide();
        }

        protected virtual void ModifyShowAnimation(Sequence animation) { }

        protected virtual void ModifyHideAnimation(Sequence animation) { }

        protected virtual void OnPreShow() { }

        protected virtual void OnPostShow() { }

        protected virtual void OnPreHide() { }

        protected virtual void OnPostHide() { }

        private void OnDestroy() => KillCurrentAnimation();

        private void KillCurrentAnimation()
        {
            if (_currentAnimation != null)
                _currentAnimation.Kill();
        }
    }
}
