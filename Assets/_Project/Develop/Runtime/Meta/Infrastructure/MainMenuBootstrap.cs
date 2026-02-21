using System.Collections;
using Assets._Project.Develop.Runtime.Configs;
using Assets._Project.Develop.Runtime.Configs.Gameplay;
using Assets._Project.Develop.Runtime.Gameplay.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.InputSystem;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuBootstrap : SceneBootstrap
    {
        private DIContainer _container;
        private MenuConfig _menuConfig;
        private LevelConfigs _levelConfigs;
        private MainMenuInputHandler _input;
        
        private bool _isRunning = false;

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            MainMenuContextRegistration.Process(_container);
        }

        public override IEnumerator Initialize()
        {
            Debug.Log("Main Menu scene initialization");

            ConfigsProviderService configsProviderService = _container.Resolve<ConfigsProviderService>();

            _menuConfig = configsProviderService.GetConfig<MenuConfig>();
            _levelConfigs = configsProviderService.GetConfig<LevelConfigs>();

            _input = _container.Resolve<MainMenuInputHandler>();

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

            if (_input.DigitsGameModeSelected)
                SwitchToGameplay(new GameplayInputArgs(_levelConfigs.GetLevelConfig(GameplayMode.Digits)));
            else if (_input.LettersGameModeSelected)
                SwitchToGameplay(new GameplayInputArgs(_levelConfigs.GetLevelConfig(GameplayMode.Letters)));
        }

        private void SwitchToGameplay(GameplayInputArgs inputArgs)
        {
            SceneSwitcherService sceneSwitcherService = _container.Resolve<SceneSwitcherService>();
            ICoroutinesPerformer coroutinesPerformer = _container.Resolve<ICoroutinesPerformer>();
            coroutinesPerformer.StartPerform(sceneSwitcherService.ProcessSwitchTo(Scenes.Gameplay, inputArgs));
        }
    }
}
