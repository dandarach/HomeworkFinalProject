using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay
{
    public class TestGameplay : MonoBehaviour
    {
        private DIContainer _container;
        private EntitiesFactory _entitiesFactory;
        private Entity _entity;

        private bool _isRunning;

        public void Initialize(DIContainer container)
        {
            _container = container;
            _entitiesFactory = _container.Resolve<EntitiesFactory>();
        }

        public void Run()
        {
            Debug.LogWarning("*** TEST GAMEPLAY ***");

            _entity = _entitiesFactory.CreateHero(Vector3.zero);

            _entitiesFactory.CreateGhost(Vector3.zero + Vector3.forward * 5f);
            _entitiesFactory.CreateGhost(Vector3.zero + Vector3.back * 5f);
            _entitiesFactory.CreateGhost(Vector3.zero + Vector3.left * 5f);
            _entitiesFactory.CreateGhost(Vector3.zero + Vector3.right * 5f);
            _entitiesFactory.CreateGhost(Vector3.zero + Vector3.forward * 2.5f + Vector3.right * 2.5f);
            _entitiesFactory.CreateGhost(Vector3.zero + Vector3.forward * 2.5f + Vector3.left * 2.5f);
            _entitiesFactory.CreateGhost(Vector3.zero + Vector3.back * 2.5f + Vector3.right * 2.5f);
            _entitiesFactory.CreateGhost(Vector3.zero + Vector3.back * 2.5f + Vector3.left * 2.5f);

            _isRunning = true;
        }

        public void Update()
        {
            if (_isRunning == false)
                return;

            if (Input.GetKeyDown(KeyCode.Space))
                _entity.StartTeleportationRequest.Invoke();
        }
    }
}
