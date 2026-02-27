using Assets._Project.Develop.Runtime.Configs.Gameplay.Levels;
using Assets._Project.Develop.Runtime.Gameplay.Statistics;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Process
{
    public class GameplayCycle : IGameplayCycle
    {
        private const string RestartGameMessage = "TO RESTART THE GAME";
        private const string GoToMainMenuMessage = "FOR MAIN MENU";

        private readonly GameplayProcess _gameplayProcess;
        private readonly SceneSwitcherService _sceneSwitcher;
        private readonly ICoroutinesPerformer _coroutinesPerformer;
        private readonly GameplayProgressService _gameplayProgressService;

        private LevelConfig _levelConfig;
        private GameState _gameState;

        public GameplayCycle(
            GameplayProcess gameplayProcess,
            SceneSwitcherService sceneSwitcher,
            ICoroutinesPerformer coroutinesPerformer,
            GameplayProgressService gameplayProgressService)
        {
            _gameplayProcess = gameplayProcess;
            _sceneSwitcher = sceneSwitcher;
            _coroutinesPerformer = coroutinesPerformer;
            _gameplayProgressService = gameplayProgressService;
        }

        public void Run(LevelConfig config)
        {
            _levelConfig = config;

            _gameplayProcess.OnWin += OnWin;
            _gameplayProcess.OnDefeat += OnDefeat;

            _gameState = GameState.Running;
            _gameplayProcess.Run(_levelConfig.Symbols, _levelConfig.SymbolsToGuess);
        }

        public void Update()
        {
            _gameplayProcess.Update();

            if (_gameState == GameState.Running)
                return;

            if (Input.GetKeyDown(_levelConfig.RestartGameKey))
            {
                if (_gameState == GameState.Win)
                    SwitchToMainMenu();
                else if (_gameState == GameState.Defeat)
                    Restart();
            }
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
            _gameState = GameState.Win;
            _gameplayProgressService.IncreaseWinCount();


            Debug.LogWarning("*** WIN ***");
            Debug.LogWarning($"PRESS {_levelConfig.RestartGameKey} {GoToMainMenuMessage}");

            OnGameEnded();
        }

        private void OnDefeat()
        {
            _gameState = GameState.Defeat;
            _gameplayProgressService.IncreaseLoseCount();

            Debug.LogWarning("*** DEFEAT ***");
            Debug.LogWarning($"PRESS {_levelConfig.RestartGameKey} {RestartGameMessage}");

            OnGameEnded();
        }

        private void OnGameEnded()
        {
            _gameplayProcess.OnWin -= OnWin;
            _gameplayProcess.OnDefeat -= OnDefeat;
            _gameplayProcess.Dispose();
        }
    }
}
