using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.Features.InputFeature;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using Assets._Project.Develop.Runtime.Utilities.StateMachineCore;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.AI.States
{
    public class PlayerInputRotationState : State, IUpdatableState
    {
        private IInputService _inputService;
        private ReactiveVariable<Vector3> _rotationDirection;
        private float _currentRotationY;

        public PlayerInputRotationState(
            Entity entity,
            IInputService inputService)
        {
            _inputService = inputService;
            _rotationDirection = entity.RotationDirection;

            //if (_rotationDirection.Value.sqrMagnitude > 0.001f)
            //    _currentRotationY = Quaternion.LookRotation(_rotationDirection.Value).eulerAngles.y;
        }

        public void Update(float deltaTime)
        {
            _currentRotationY += _inputService.XAxis;
            Quaternion finalRotation = Quaternion.Euler(0, _currentRotationY, 0);
            _rotationDirection.Value = finalRotation * Vector3.forward;
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
