using System;
using Assets._Project.Develop.Runtime.UI.CommonViews;
using Assets._Project.Develop.Runtime.UI.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Develop.Runtime.UI.MainMenu
{
    public class MainMenuScreenView : MonoBehaviour, IView
    {
        public event Action OpenLevelsMenuButtonClicked;

        [field: SerializeField] public IconTextListView WalletView { get; private set; }
        [field: SerializeField] public IconTextListView StatsView { get; private set; }

        [SerializeField] private Button _openLevelsMenuButton;
        [SerializeField] private Button _resetStatsMenuButton;

        private void OnEnable()
        {
            _openLevelsMenuButton.onClick.AddListener(OnOpenLevelsMenuButtonClicked);
            _resetStatsMenuButton.onClick.AddListener(OnResetStatsMenuButtonClicked);
        }

        private void OnDisable()
        {
            _openLevelsMenuButton.onClick.RemoveListener(OnOpenLevelsMenuButtonClicked);
            _resetStatsMenuButton.onClick.RemoveListener(OnResetStatsMenuButtonClicked);
        }

        private void OnOpenLevelsMenuButtonClicked() => OpenLevelsMenuButtonClicked?.Invoke();

        private void OnResetStatsMenuButtonClicked()
        {
            Debug.LogWarning("OnResetStatsMenuButtonClicked");
        }
    }
}
