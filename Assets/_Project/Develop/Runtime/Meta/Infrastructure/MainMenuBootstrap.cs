using System.Collections;
using Assets._Project.Develop.Runtime.Gameplay.Configs;
using Assets._Project.Develop.Runtime.Gameplay.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuBootstrap : SceneBootstrap
    {
        private DIContainer _container;
        private bool _isRunning = false;
        private GameConfig _gameConfig;

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            MainMenuContextRegistration.Process(_container);
        }

        public override IEnumerator Initialize()
        {
            Debug.Log("Main Menu scene initialization");

            yield break;
        }

        public override void Run()
        {
            Debug.Log("Main Menu scene start");

            _gameConfig = _container.Resolve<ConfigsProviderService>().GetConfig<GameConfig>();

            _isRunning = true;
        }

        private void Update()
        {
            if (_isRunning == false)
                return;

            if (Input.GetKeyDown(KeyCode.Alpha1))
                SwitchToGameplay(new GameplayInputArgs(_gameConfig.NumbersList, _gameConfig.SymbolsCount));
            else if (Input.GetKeyDown(KeyCode.Alpha2))
                SwitchToGameplay(new GameplayInputArgs(_gameConfig.LettersList, _gameConfig.SymbolsCount));
        }

        private void SwitchToGameplay(GameplayInputArgs inputArgs)
        {
            SceneSwitcherService sceneSwitcherService = _container.Resolve<SceneSwitcherService>();
            ICoroutinesPerformer coroutinesPerformer = _container.Resolve<ICoroutinesPerformer>();
            coroutinesPerformer.StartPerform(sceneSwitcherService.ProcessSwitchTo(Scenes.Gameplay, inputArgs));
        }
    }
}
