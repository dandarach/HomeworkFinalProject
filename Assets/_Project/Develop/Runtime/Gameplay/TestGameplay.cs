using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.Features.AI;
using Assets._Project.Develop.Runtime.Gameplay.Features.AI.States;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay
{
    public class TestGameplay : MonoBehaviour
    {
        private DIContainer _container;
        private EntitiesFactory _entitiesFactory;
        private BrainsFactory _brainsFactory;

        private Entity _hero1;
        private Entity _hero2;
        private Entity _hero3;
        private Entity _ghost1;

        private bool _isRunning;

        public void Initialize(DIContainer container)
        {
            _container = container;
            _entitiesFactory = _container.Resolve<EntitiesFactory>();
            _brainsFactory = _container.Resolve<BrainsFactory>();
        }

        public void Run()
        {
            Debug.LogWarning("*** TEST GAMEPLAY ***");

            _entitiesFactory.CreateGhost(Vector3.zero + Vector3.forward * 2.5f + Vector3.right * 2.5f, "Ghost1", 50f);
            _entitiesFactory.CreateGhost(Vector3.zero + Vector3.forward * 5f, "Ghost5", 100f);
            _entitiesFactory.CreateGhost(Vector3.zero + Vector3.back * 5f, "Ghost2", 90f);
            _entitiesFactory.CreateGhost(Vector3.zero + Vector3.left * 5f, "Ghost3", 80f);
            _entitiesFactory.CreateGhost(Vector3.zero + Vector3.right * 5f, "Ghost4", 70f);
            _entitiesFactory.CreateGhost(Vector3.zero + Vector3.forward * 2.5f + Vector3.left * 2.5f, "Ghost6", 60f);
            _entitiesFactory.CreateGhost(Vector3.zero + Vector3.back * 2.5f + Vector3.right * 2.5f, "Ghost7");
            _entitiesFactory.CreateGhost(Vector3.zero + Vector3.back * 2.5f + Vector3.left * 2.5f, "Ghost8");

            _hero1 = _entitiesFactory.CreateHero(Vector3.zero, "Hero1");
            _brainsFactory.CreateRandomTeleportationBrain(_hero1, 3f, 5f);

            _hero2 = _entitiesFactory.CreateHero(Vector3.zero + Vector3.back * 2f, "Hero2");
            _hero2.AddCurrentTarget();
            _brainsFactory.CreateTeleportationToTragetWithMinHealthBrain(_hero2, 2f, 2f, new MinimalHealthTargetSelector(_hero2));

            _hero3 = _entitiesFactory.CreateHero(Vector3.zero + Vector3.forward * 2f, "Hero3");
            _brainsFactory.CreateMainHeroBrain(_hero3, new MinimalHealthTargetSelector(_hero3));

            _isRunning = true;
        }

        public void Update()
        {
            if (_isRunning == false)
                return;

            if (Input.GetKeyDown(KeyCode.Alpha1))
                _brainsFactory.CreateRandomTeleportationBrain(_hero3, 2f, 5f);

            if (Input.GetKeyDown(KeyCode.Alpha2))
                _brainsFactory.CreateGhostBrain(_hero3);

            if (Input.GetKeyDown(KeyCode.Alpha3))
                _brainsFactory.CreateMainHeroBrain(_hero3, new NearestDamageableTargetSelector(_hero3));
        }
    }
}
