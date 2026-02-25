using System;
using System.Collections;
using Assets._Project.Develop.Runtime.Gameplay.Process;
using Assets._Project.Develop.Runtime.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayBootstrap : SceneBootstrap
    {
        private DIContainer _container;
        private GameplayInputArgs _inputArgs;
        private IGameplayCycle _gameplayCycle;

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            if (sceneArgs is not GameplayInputArgs gameplayInputArgs)
                throw new ArgumentException($"{nameof(sceneArgs)} is not match with {typeof(GameplayInputArgs)} type");

            _inputArgs = gameplayInputArgs;

            GameplayContextRegistration.Process(_container);
        }

        public override IEnumerator Initialize()
        {
            Debug.Log($"Gameplay mode symbols: '{_inputArgs.Symbols}', SymbolsToGuess: {_inputArgs.SymbolsToGuess}");

            _gameplayCycle = _container.Resolve<IGameplayCycle>();

            yield break;
        }

        public override void Run()
        {
            Debug.Log("Gameplay scene start");

            _gameplayCycle.Run(_inputArgs);
        }

        private void Update()
        {
            if (_gameplayCycle == null)
                return;

            _gameplayCycle.Update();
        }
    }
}
