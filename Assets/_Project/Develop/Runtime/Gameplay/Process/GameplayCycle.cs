using Assets._Project.Develop.Runtime.Configs.Gameplay.Levels;
using Assets._Project.Develop.Runtime.Meta.Features.Stats;
using Assets._Project.Develop.Runtime.UI.Gameplay;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Process
{
    public class GameplayCycle : IGameplayCycle
    {
        private const string WinPopupTitle = "GAME OVER";
        private const string WinMessage = "YOU WIN!";
        private const string DefeatPopupTitle = "GAME OVER";
        private const string DefeatMessage = "DEFEAT!";

        private readonly GameplayProcess _gameplayProcess;
        private readonly SceneSwitcherService _sceneSwitcher;
        private readonly ICoroutinesPerformer _coroutinesPerformer;
        private readonly GameplayProgressService _gameplayProgressService;
        private readonly GameplayPopupService _gameplayPopupService;

        private LevelConfig _levelConfig;

        public GameplayCycle(
            GameplayProcess gameplayProcess,
            SceneSwitcherService sceneSwitcher,
            ICoroutinesPerformer coroutinesPerformer,
            GameplayProgressService gameplayProgressService,
            GameplayPopupService gameplayPopupService)
        {
            _gameplayProcess = gameplayProcess;
            _sceneSwitcher = sceneSwitcher;
            _coroutinesPerformer = coroutinesPerformer;
            _gameplayProgressService = gameplayProgressService;
            _gameplayPopupService = gameplayPopupService;
        }

        public void Run(LevelConfig config)
        {
            _levelConfig = config;

            _gameplayProcess.OnWin += OnWin;
            _gameplayProcess.OnDefeat += OnDefeat;

            _gameplayProcess.Run(_levelConfig.Symbols, _levelConfig.SymbolsToGuess);
        }

        public void Update()
        {
            _gameplayProcess.Update();
        }

        private void Restart()
        {
            OnGameEnded();
            Run(_levelConfig);
        }

        private void SwitchToMainMenu()
        {
            _coroutinesPerformer.StartPerform(_sceneSwitcher.ProcessSwitchTo(Scenes.MainMenu));
        }

        private void OnWin()
        {
            _gameplayProgressService.ProcessWin();

            Debug.LogWarning("*** WIN ***");

            OnGameEnded();

            _gameplayPopupService.OpenPopup(WinPopupTitle, WinMessage);
        }

        private void OnDefeat()
        {
            _gameplayProgressService.ProcessDefeat();

            Debug.LogWarning("*** DEFEAT ***");
            
            OnGameEnded();

            _gameplayPopupService.OpenPopup(DefeatPopupTitle, DefeatMessage);
        }

        private void OnGameEnded()
        {
            _gameplayProcess.OnWin -= OnWin;
            _gameplayProcess.OnDefeat -= OnDefeat;
            _gameplayProcess.Dispose();
        }
    }
}
