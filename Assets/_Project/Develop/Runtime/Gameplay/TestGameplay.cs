using Assets._Project.Develop.Runtime.Configs.Gameplay.Entities;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.Features.AI;
using Assets._Project.Develop.Runtime.Gameplay.Features.AI.States;
using Assets._Project.Develop.Runtime.Gameplay.Features.Enemies;
using Assets._Project.Develop.Runtime.Gameplay.Features.MainHero;
using Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeature;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay
{
    public class TestGameplay : MonoBehaviour
    {
        [SerializeField] private HeroConfig _heroConfig;
        [SerializeField] private GhostConfig _ghostConfig;

        private DIContainer _container;
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
        }

        public void Run()
        {
            _entity = _mainHeroFactory.Create(Vector3.zero);
            _ghost = _enemiesFactory.Create(Vector3.zero + Vector3.forward * 5f, _ghostConfig);

            _isRunning = true;
        }

        public void Update()
        {
            if (_isRunning == false)
                return;
        }
    }
}
