using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeature
{
    public class CharacterControllerRotationtSystem : IInitializableSystem, IUpdatableSystem
    {
        private ReactiveVariable<Quaternion> _targetRotation;
        private ReactiveVariable<Quaternion> _currentRotation;
        private CharacterController _controller;

        public void OnInit(Entity entity)
        {
            _targetRotation = entity.TargetRotation;
            _controller = entity.CharacterController;
            _currentRotation = entity.CurrentRotation;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_targetRotation.Value == Quaternion.identity)
                return;

            _controller.transform.rotation = _targetRotation.Value;
            _currentRotation.Value = _controller.transform.rotation;
        }
    }
}
