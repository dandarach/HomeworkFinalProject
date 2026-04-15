using System;
using System.Collections;
using Assets._Project.Develop.Runtime.Configs.Gameplay.Levels;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.Features.AI;
using Assets._Project.Develop.Runtime.Gameplay.Process;
using Assets._Project.Develop.Runtime.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayBootstrap : SceneBootstrap
    {
        [SerializeField] private TestGameplay _testGameplay;

        private DIContainer _container;
        private GameplayInputArgs _inputArgs;
        private LevelConfig _levelConfig;
        private GameplayEconomyService _economyService;
        private EntitiesLifeContext _entitiesLifeContext;
        private AIBrainsContext _brainsContext;

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            if (sceneArgs is not GameplayInputArgs gameplayInputArgs)
                throw new ArgumentException($"{nameof(sceneArgs)} is not match with {typeof(GameplayInputArgs)} type");

            _inputArgs = gameplayInputArgs;

            GameplayContextRegistration.Process(_container, _inputArgs);
        }

        public override IEnumerator Initialize()
        {
            //LevelsListConfig levelConfigs = _container.Resolve<ConfigsProviderService>().GetConfig<LevelsListConfig>();
            //_levelConfig = levelConfigs.GetLevelConfig(_inputArgs.GameplayMode);

            _economyService = _container.Resolve<GameplayEconomyService>();

            _entitiesLifeContext = _container.Resolve<EntitiesLifeContext>();
            _brainsContext = _container.Resolve<AIBrainsContext>();

            _testGameplay.Initialize(_container);

            yield break;
        }

        public override void Run()
        {
            Debug.Log("Gameplay scene start");

            _economyService.Initialize(_levelConfig.WinAward);

            _testGameplay.Run();
        }

        private void Update()
        {
            _brainsContext?.Update(Time.deltaTime);
            _entitiesLifeContext?.Update(Time.deltaTime);
        }
    }
}
