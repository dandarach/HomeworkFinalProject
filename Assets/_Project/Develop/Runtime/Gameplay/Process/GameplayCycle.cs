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
        private readonly GameplayProcess _gameplayProcess;
        private readonly GameplayProgressService _gameplayProgressService;
        private readonly GameplayPopupService _gameplayPopupService;

        private LevelConfig _levelConfig;

        public GameplayCycle(
            GameplayProcess gameplayProcess,
            GameplayProgressService gameplayProgressService,
            GameplayPopupService gameplayPopupService)
        {
            _gameplayProcess = gameplayProcess;
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

        private void OnWin()
        {
            _gameplayProgressService.ProcessWin();

            Debug.LogWarning("*** WIN ***");

            OnGameEnded();

            _gameplayPopupService.OpenWinPopup();
        }

        private void OnDefeat()
        {
            _gameplayProgressService.ProcessDefeat();

            Debug.LogWarning("*** DEFEAT ***");
            
            OnGameEnded();

            _gameplayPopupService.OpenDefeatPopup(Restart);
        }

        private void OnGameEnded()
        {
            _gameplayProcess.OnWin -= OnWin;
            _gameplayProcess.OnDefeat -= OnDefeat;
            _gameplayProcess.Dispose();
        }
    }
}
