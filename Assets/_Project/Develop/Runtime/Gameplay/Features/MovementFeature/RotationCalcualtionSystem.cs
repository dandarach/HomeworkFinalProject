using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeature
{
    public class RotationCalcualtionSystem : IInitializableSystem, IUpdatableSystem
    {
        private ReactiveVariable<Vector3> _direction;
        private ReactiveVariable<float> _rotationSpeed;
        private ReactiveVariable<Quaternion> _targetRotation;
        private ReactiveVariable<Quaternion> _currentRotation;

        public void OnInit(Entity entity)
        {
            _direction = entity.MoveDirection;
            _rotationSpeed = entity.RotationSpeed;
            _currentRotation = entity.CurrentRotation;
            _targetRotation = entity.TargetRotation;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_direction.Value == Vector3.zero)
                return;

            Quaternion lookRotation = Quaternion.LookRotation(_direction.Value.normalized);

            float step = _rotationSpeed.Value * deltaTime;

            _targetRotation.Value = Quaternion.RotateTowards(
                _currentRotation.Value,
                lookRotation,
                step
            );
        }
    }
}
