using System;
using System.Collections;
using Assets._UnityAdvantureProject.Develop.Runtime.Infrastructure;
using Assets._UnityAdvantureProject.Develop.Runtime.Infrastructure.DI;
using Assets._UnityAdvantureProject.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._UnityAdvantureProject.Develop.Runtime.Utilities.SceneManagement;
using UnityEngine;

namespace Assets._UnityAdvantureProject.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayBootstrap : SceneBootstrap
    {
        private DIContainer _container;
        private GameplayInputArgs _inputArgs;

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
            Debug.Log($"You are on the level {_inputArgs.LevelNumber}");

            Debug.Log("Gameplay scene initialization");

            yield break;
        }

        public override void Run()
        {
            Debug.Log("Gameplay scene start");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                SceneSwitcherService sceneSwitcherService = _container.Resolve<SceneSwitcherService>();
                ICoroutinesPerformer coroutinesPerformer = _container.Resolve<ICoroutinesPerformer>();
                coroutinesPerformer.StartPerform(sceneSwitcherService.ProcessSwitchTo(Scenes.MainMenu));
            }
        }
    }
}
