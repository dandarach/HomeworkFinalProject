using UnityEngine;
using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using Assets._Project.Develop.Runtime.Gameplay.Infrastructure;

namespace Assets._Project.Develop.Runtime.UI.LevelsMenuPopup
{
    public class LevelTilePresenter : ISubscibedPresenter
    {
        private readonly SceneSwitcherService _sceneSwitcherService;
        private readonly ICoroutinesPerformer _coroutinesPerformer;

        private readonly int _levelNumber;
        private readonly LevelTileView _view;

        public LevelTilePresenter(
            SceneSwitcherService sceneSwitcherService,
            ICoroutinesPerformer coroutinesPerformer,
            int levelNumber,
            LevelTileView view)
        {
            _sceneSwitcherService = sceneSwitcherService;
            _coroutinesPerformer = coroutinesPerformer;
            _levelNumber = levelNumber;
            _view = view;
        }

        public LevelTileView View => _view;

        public void Initialize()
        {
            _view.SetLevel(_levelNumber.ToString());
            _view.Activate();
        }

        public void Dispose()
        {
            _view.Clicked -= OnViewClicked;
        }

        public void Subscribe()
        {
            _view.Clicked += OnViewClicked;
        }

        public void Unsubscribe()
        {
            _view.Clicked -= OnViewClicked;
        }

        private void OnViewClicked()
        {
            // TODO: NEED TO FIX
            _coroutinesPerformer
                .StartPerform(_sceneSwitcherService.ProcessSwitchTo(Scenes.Gameplay, new GameplayInputArgs(Configs.Gameplay.GameplayMode.Digits)));
        }
    }
}
