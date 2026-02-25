using System.Collections;
using Assets._Project.Develop.Runtime.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta.GameModeChoose;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure.MainMenu
{
    public class MainMenuBootstrap : SceneBootstrap
    {
        private DIContainer _container;
        private IGameModeChooseService _gameModeChooseService;

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

            _gameModeChooseService = _container.Resolve<IGameModeChooseService>();

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

            _gameModeChooseService.Update();
        }
    }
}
