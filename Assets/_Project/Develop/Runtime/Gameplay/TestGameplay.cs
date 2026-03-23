using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.Features.InputSystem;
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
        private Entity _activeEntity;
        private IInputService _inputService;

        private bool _isRunning;

        public void Initialize(DIContainer container)
        {
            _container = container;
            _entitiesFactory = _container.Resolve<EntitiesFactory>();
            _inputService = _container.Resolve<IInputService>();
        }

        public void Run()
        {
            _rigidbodyTestEntity = _entitiesFactory.CreateRigidbodyEntity(Vector3.zero);
            _characterControllerTestEntity = _entitiesFactory.CreateCharacterControllerEntity(Vector3.zero);

            _activeEntity = _rigidbodyTestEntity;
            _isRunning = true;
        }

        public void Update()
        {
            if (_isRunning == false)
                return;

            if (_inputService.IsSwitchEntityButtonPressed)
                SwitchEntity();
            else
                MoveEntity();
        }

        private void MoveEntity()
        {
            Vector3 input = _inputService.Direction;

            _activeEntity.MoveDirection.Value = input;
        }

        private void SwitchEntity()
        {
            _activeEntity.MoveDirection.Value = Vector3.zero;

            if (_activeEntity == _rigidbodyTestEntity)
                _activeEntity = _characterControllerTestEntity;
            else
                _activeEntity = _rigidbodyTestEntity;

            Debug.Log($"Active entity switched to: {_activeEntity}");
        }
    }
}
