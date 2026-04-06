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

        private Entity _entity;
        private Entity _ghost;

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

            _entity = _entitiesFactory.CreateHero(Vector3.zero, "Hero");
            _entity.AddCurrentTarget();
            
            _brainsFactory.CreateMainHeroBrain(_entity, new NearestDamageableTargetSelector(_entity));

            _ghost = _entitiesFactory.CreateGhost(Vector3.zero + Vector3.forward * 5f, "Ghost1");
            _entitiesFactory.CreateGhost(Vector3.zero + Vector3.back * 5f, "Ghost2");
            _entitiesFactory.CreateGhost(Vector3.zero + Vector3.left * 5f, "Ghost3");
            _entitiesFactory.CreateGhost(Vector3.zero + Vector3.right * 5f, "Ghost4");
            _entitiesFactory.CreateGhost(Vector3.zero + Vector3.forward * 2.5f + Vector3.right * 2.5f, "Ghost5");
            _entitiesFactory.CreateGhost(Vector3.zero + Vector3.forward * 2.5f + Vector3.left * 2.5f, "Ghost6");
            _entitiesFactory.CreateGhost(Vector3.zero + Vector3.back * 2.5f + Vector3.right * 2.5f, "Ghost7");
            _entitiesFactory.CreateGhost(Vector3.zero + Vector3.back * 2.5f + Vector3.left * 2.5f, "Ghost8");

            _isRunning = true;
        }

        public void Update()
        {
            if (_isRunning == false)
                return;

            if (Input.GetKeyDown(KeyCode.Space))
                _entity.StartTeleportationRequest.Invoke();

            if (Input.GetKeyDown(KeyCode.F))
                _entity.EndTeleportationEvent.Invoke();

            if (Input.GetKeyDown(KeyCode.I))
                _brainsFactory.CreateGhostBrain(_ghost);
        }
    }
}
