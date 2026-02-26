using Assets._Project.Develop.Runtime.Configs.Gameplay.Levels;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Process
{
    public class GameplayCycle : IGameplayCycle
    {
        private const string RestartGameMessage = "TO RESTART THE GAME";
        private const string GoToMainMenuMessage = "FOR MAIN MENU";

        private GameplayProcess _gameplayProcess;
        private SceneSwitcherService _sceneSwitcher;
        private ICoroutinesPerformer _coroutinesPerformer;

        private LevelConfig _levelConfig;
        private GameState _gameState;

        public GameplayCycle(
            GameplayProcess gameplayProcess,
            SceneSwitcherService sceneSwitcher,
            ICoroutinesPerformer coroutinesPerformer)
        {
            _gameplayProcess = gameplayProcess;
            _sceneSwitcher = sceneSwitcher;
            _coroutinesPerformer = coroutinesPerformer;
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
            Debug.LogWarning("*** WIN ***");
            Debug.LogWarning($"PRESS {_levelConfig.RestartGameKey} {GoToMainMenuMessage}");
            _gameState = GameState.Win;
            OnGameEnded();
        }

        private void OnDefeat()
        {
            Debug.LogWarning("*** DEFEAT ***");
            Debug.LogWarning($"PRESS {_levelConfig.RestartGameKey} {RestartGameMessage}");
            _gameState = GameState.Defeat;
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
