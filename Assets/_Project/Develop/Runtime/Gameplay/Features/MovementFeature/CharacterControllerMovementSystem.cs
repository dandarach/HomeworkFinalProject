using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeature
{
    public class CharacterControllerMovementSystem : IInitializableSystem, IUpdatableSystem
    {
        private ReactiveVariable<Vector3> _velocity;
        private CharacterController _characterController;

        public void OnInit(Entity entity)
        {
            _velocity = entity.Velocity;
            _characterController = entity.CharacterController;
        }

        public void OnUpdate(float deltaTime)
        {
            _characterController.Move(_velocity.Value * deltaTime);
        }
    }
}
