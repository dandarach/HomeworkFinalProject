using System;
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
        private IInputService _inputService;

        private bool _isRunning;
        private EntityType _currentEntityType;

        public void Initialize(DIContainer container)
        {
            _container = container;
            _entitiesFactory = _container.Resolve<EntitiesFactory>();
            _currentEntityType = EntityType.Rigidbody;
            _inputService = _container.Resolve<IInputService>();
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

            if (_inputService.IsSwitchEntityButtonPressed)
                SwitchEntity();

            MoveEntity();
        }

        private void MoveEntity()
        {
            Vector3 input = _inputService.Direction;

            if (_currentEntityType == EntityType.Rigidbody)
                _rigidbodyTestEntity.MoveDirection.Value = input;
            else
                _characterControllerTestEntity.MoveDirection.Value = input;
        }

        private void SwitchEntity()
        {
            if (_currentEntityType == EntityType.Rigidbody)
                _currentEntityType = EntityType.CharacterController;
            else
                _currentEntityType = EntityType.Rigidbody;
        }
    }

    public enum EntityType
    {
        Rigidbody,
        CharacterController
    }
}
