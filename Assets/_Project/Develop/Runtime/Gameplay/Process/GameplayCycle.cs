using System;
using Assets._Project.Develop.Runtime.Gameplay.Infrastructure;
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

        private GameplayInputArgs _inputArgs;
        private GameState _gameState;
        private KeyCode _restartGameKey;

        public GameplayCycle(
            GameplayProcess gameplayProcess,
            SceneSwitcherService sceneSwitcher,
            ICoroutinesPerformer coroutinesPerformer,
            KeyCode restartGameKey)
        {
            _gameplayProcess = gameplayProcess;
            _sceneSwitcher = sceneSwitcher;
            _coroutinesPerformer = coroutinesPerformer;
            _restartGameKey = restartGameKey;
        }

        public void Run(GameplayInputArgs args)
        {
            _inputArgs = args;

            _gameplayProcess.OnWin += OnWin;
            _gameplayProcess.OnDefeat += OnDefeat;

            _gameState = GameState.Running;
            _gameplayProcess.Run(args);
        }

        public void Update()
        {
            _gameplayProcess.Update();

            if (_gameState == GameState.Running)
                return;

            if (Input.GetKeyDown(_restartGameKey))
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
            Run(_inputArgs);
        }

        private void SwitchToMainMenu()
        {
            _coroutinesPerformer.StartPerform(_sceneSwitcher.ProcessSwitchTo(Scenes.MainMenu));
        }

        private void OnWin()
        {
            Debug.LogWarning("*** WIN ***");
            Debug.LogWarning($"PRESS {_restartGameKey} {GoToMainMenuMessage}");
            _gameState = GameState.Win;
            OnGameEnded();
        }

        private void OnDefeat()
        {
            Debug.LogWarning("*** DEFEAT ***");
            Debug.LogWarning($"PRESS {_restartGameKey} {RestartGameMessage}");
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
