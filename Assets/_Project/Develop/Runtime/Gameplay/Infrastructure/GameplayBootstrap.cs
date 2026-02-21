using System;
using System.Collections;
using Assets._Project.Develop.Runtime.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayBootstrap : SceneBootstrap
    {
        private DIContainer _container;
        private GameplayInputArgs _inputArgs;
        private GameplayProcess _gameplayProcess;

        private GameState _gameState;

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            if (sceneArgs is not GameplayInputArgs gameplayInputArgs)
                throw new ArgumentException($"{nameof(sceneArgs)} is not match with {typeof(GameplayInputArgs)} type");

            _inputArgs = gameplayInputArgs;

            GameplayContextRegistration.Process(_container, gameplayInputArgs);
        }

        public override IEnumerator Initialize()
        {
            Debug.Log($"Gameplay mode symbols: '{_inputArgs.Symbols}', SymbolsToGuess: {_inputArgs.SymbolsToGuess}");

            //_gameplayProcess = _container.Resolve<GameplayProcess>();
            
            yield break;
        }

        public override void Run()
        {
            Debug.Log("Gameplay scene start");

            _gameplayProcess = _container.Resolve<GameplayProcess>();

            _gameplayProcess.OnWin += OnGameWin;
            _gameplayProcess.OnDefeat += OnGameDefeat;

            _gameState = GameState.Running;
            _gameplayProcess.Run();
        }

        private void Update()
        {
            _gameplayProcess?.Update();

            if (_gameState == GameState.Running)
                return;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (_gameState == GameState.Win)
                    SwitchToMainMenu();

                if (_gameState == GameState.Defeat)
                    RestartGame();
            }
        }

        private void SwitchToMainMenu()
        {
            Debug.Log("Switch to Main Menu");

            SceneSwitcherService sceneSwitcherService = _container.Resolve<SceneSwitcherService>();
            ICoroutinesPerformer coroutinesPerformer = _container.Resolve<ICoroutinesPerformer>();
            coroutinesPerformer.StartPerform(sceneSwitcherService.ProcessSwitchTo(Scenes.MainMenu));
        }

        private void RestartGame()
        {
            Debug.Log("RestartGame");
            Run();
        }

        private void OnGameWin()
        {
            Debug.LogWarning("*** WIN ***");

            OnGameEnded();
            _gameState = GameState.Win;
        }

        private void OnGameDefeat()
        {
            Debug.LogWarning("*** DEFEAT ***");

            OnGameEnded();
            _gameState = GameState.Defeat;
        }

        private void OnGameEnded()
        {
            _gameplayProcess.OnWin -= OnGameWin;
            _gameplayProcess.OnDefeat -= OnGameDefeat;

            _gameplayProcess.Dispose();
        }
    }
}
