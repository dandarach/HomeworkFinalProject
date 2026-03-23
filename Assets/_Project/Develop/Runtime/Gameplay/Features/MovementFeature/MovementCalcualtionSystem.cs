using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeature
{
    public class MovementCalcualtionSystem : IInitializableSystem, IUpdatableSystem
    {
        private ReactiveVariable<Vector3> _inputDirection;
        private ReactiveVariable<float> _speed;
        private ReactiveVariable<Vector3> _velocity;

        public void OnInit(Entity entity)
        {
            _inputDirection = entity.MoveDirection;
            _speed = entity.MoveSpeed;
            _velocity = entity.Velocity;
        }

        public void OnUpdate(float deltaTime)
        {
            _velocity.Value = _inputDirection.Value.normalized * _speed.Value;
        }
    }
}
