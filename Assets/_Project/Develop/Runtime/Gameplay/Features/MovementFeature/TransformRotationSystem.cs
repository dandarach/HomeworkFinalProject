using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeature
{
    public class TransformRotationtSystem : IInitializableSystem, IUpdatableSystem
    {
        private ReactiveVariable<Vector3> _direction;
        private ReactiveVariable<float> _rotationSpeed;
        private CharacterController _characterController;

        public void OnInit(Entity entity)
        {
            _direction = entity.MoveDirection;
            _rotationSpeed = entity.RotationSpeed;
            _characterController = entity.CharacterController;

            if (_direction.Value != Vector3.zero)
                _characterController.transform.rotation = Quaternion.LookRotation(_direction.Value.normalized);
        }

        public void OnUpdate(float deltaTime)
        {
            if (_direction.Value == Vector3.zero)
                return;

            Quaternion lookRotation = Quaternion.LookRotation(_direction.Value.normalized);

            float step = _rotationSpeed.Value * deltaTime;

            Quaternion rotation = Quaternion.RotateTowards(_characterController.transform.rotation, lookRotation, step);

            _characterController.transform.rotation = rotation;
        }
    }
}
