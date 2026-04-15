using System;
using Assets._Project.Develop.Runtime.Configs.Gameplay.Entities;
using Assets._Project.Develop.Runtime.Configs.Gameplay.Stages;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.Features.AI;
using Assets._Project.Develop.Runtime.Gameplay.Features.Enemies;
using Assets._Project.Develop.Runtime.Gameplay.Features.MainHero;
using Assets._Project.Develop.Runtime.Gameplay.Features.StagesFeature;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay
{
    public class TestGameplay : MonoBehaviour
    {
        [SerializeField] private HeroConfig _heroConfig;
        [SerializeField] private StageConfig _stageConfig;

        private DIContainer _container;
        private StagesFactory _stagesFactory;
        private IStage _stage;
        private EntitiesFactory _entitiesFactory;
        private BrainsFactory _brainsFactory;
        private MainHeroFactory _mainHeroFactory;
        private EnemiesFactory _enemiesFactory;

        private Entity _entity;
        private Entity _ghost;

        private bool _isRunning;

        public void Initialize(DIContainer container)
        {
            _container = container;
            _entitiesFactory = _container.Resolve<EntitiesFactory>();
            _brainsFactory = _container.Resolve<BrainsFactory>();
            _mainHeroFactory = _container.Resolve<MainHeroFactory>();
            _enemiesFactory = _container.Resolve<EnemiesFactory>();
            _stagesFactory = _container.Resolve<StagesFactory>();
        }

        public void Run()
        {
            _entity = _mainHeroFactory.Create(Vector3.zero);
            
            _stage = _stagesFactory.Create(_stageConfig);
            _stage.Completed.Subscribe(OnCompleted);
            _stage.Start();

            _isRunning = true;
        }

        private void OnCompleted()
        {
            Debug.LogWarning("*** WIN! ***");
            _stage.Cleanup();
        }

        public void Update()
        {
            if (_isRunning == false)
                return;

            _stage.Update(Time.deltaTime);
        }
    }
}
