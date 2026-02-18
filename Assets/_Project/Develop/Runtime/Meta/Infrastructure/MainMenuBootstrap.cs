using System.Collections;
using Assets._Project.Develop.Runtime.Configs;
using Assets._Project.Develop.Runtime.Gameplay.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuBootstrap : SceneBootstrap
    {
        private DIContainer _container;
        private MenuConfig _menuConfig;
        //private GameConfig _gameConfig;
        
        private bool _isRunning = false;

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            MainMenuContextRegistration.Process(_container);
        }

        public override IEnumerator Initialize()
        {
            Debug.Log("Main Menu scene initialization");
            _menuConfig = _container.Resolve<ConfigsProviderService>().GetConfig<MenuConfig>();
            //_gameConfig = _container.Resolve<ConfigsProviderService>().GetConfig<GameConfig>();

            yield break;
        }

        public override void Run()
        {
            Debug.Log("Main Menu scene start");
            _isRunning = true;
        }

        private void Update()
        {
            if (_isRunning == false)
                return;

            //if (Input.GetKeyDown(_menuConfig.SelectNumbersGameModeKey))
                //SwitchToGameplay(new GameplayInputArgs(_gameConfig.NumbersList, _gameConfig.SymbolsCount));
            //else if (Input.GetKeyDown(_menuConfig.SelectLettersGameModeKey))
                //SwitchToGameplay(new GameplayInputArgs(_gameConfig.LettersList, _gameConfig.SymbolsCount));
        }

        private void SwitchToGameplay(GameplayInputArgs inputArgs)
        {
            SceneSwitcherService sceneSwitcherService = _container.Resolve<SceneSwitcherService>();
            ICoroutinesPerformer coroutinesPerformer = _container.Resolve<ICoroutinesPerformer>();
            coroutinesPerformer.StartPerform(sceneSwitcherService.ProcessSwitchTo(Scenes.Gameplay, inputArgs));
        }
    }
}
