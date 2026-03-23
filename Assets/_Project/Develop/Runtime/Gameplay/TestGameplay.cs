using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay
{
    public class TestGameplay : MonoBehaviour
    {
        private DIContainer _container;
        private EntitiesFactory _entitiesFactory;
        private Entity _rigidbodyTestEntity;
        private Entity _characterControllerTestEntity;

        private bool _isRunning;

        public void Initialize(DIContainer container)
        {
            _container = container;
            _entitiesFactory = _container.Resolve<EntitiesFactory>();
        }

        public void Run()
        {
            _rigidbodyTestEntity = _entitiesFactory.CreateRigidbodyEntity(Vector3.zero);
            _characterControllerTestEntity = _entitiesFactory.CreateCharacterControllerEntity(Vector3.zero);

            _isRunning = true;
        }

        public void Update()
        {
            if (_isRunning == false)
                return;

            Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

            _rigidbodyTestEntity.MoveDirection.Value = input;
            _characterControllerTestEntity.MoveDirection.Value = input;
        }
    }
}
