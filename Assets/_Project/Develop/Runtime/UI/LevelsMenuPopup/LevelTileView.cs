using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Assets._Project.Develop.Runtime.UI.Core;
namespace Assets._Project.Develop.Runtime.UI.LevelsMenuPopup
{
    public class LevelTileView : MonoBehaviour, IShowableView
    {
        public event Action Clicked;

        [SerializeField] private Image _background;
        [SerializeField] private TMP_Text _levelNameText;
        [SerializeField] private Button _button;

        [SerializeField] private Color _activeColor;
        [SerializeField] private Color _completedColor;
        [SerializeField] private Color _blockedColor;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        public void SetLevelName(string level) => _levelNameText.text = level;

        public void Block() => _background.color = _blockedColor;
        
        public void Complete() => _background.color = _completedColor;
        
        public void Activate() => _background.color = _activeColor;

        public Tween Show()
        {
            transform.DOKill();

            return transform
                .DOScale(1, 0.1f)
                .From(0)
                .SetUpdate(true)
                .Play();
        }

        public Tween Hide()
        {
            transform.DOKill();

            return DOTween.Sequence();
        }

        private void OnDestroy()
        {
            transform.DOKill();
        }

        private void OnClick() => Clicked?.Invoke();
    }
}
