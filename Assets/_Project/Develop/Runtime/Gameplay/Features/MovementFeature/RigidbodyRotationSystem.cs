using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeature
{
    public class RigidbodyRotationtSystem : IInitializableSystem, IUpdatableSystem
    {
        private ReactiveVariable<Quaternion> _targetRotation;
        private Rigidbody _rigidbody;

        public void OnInit(Entity entity)
        {
            _targetRotation = entity.TargetRotation;
            _rigidbody = entity.Rigidbody;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_targetRotation.Value == Quaternion.identity)
                return;

            _rigidbody.MoveRotation(_targetRotation.Value);
        }
    }
}
