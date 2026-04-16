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
        private Transform _transform;
        
        private static readonly Plane GroundPlane = new Plane(Vector3.up, Vector3.zero);

        public PlayerInputRotationState(
            Entity entity,
            IInputService inputService)
        {
            _inputService = inputService;
            _rotationDirection = entity.RotationDirection;
            _transform = entity.Transform;
        }

        public override void Enter()
        {
            //Debug.Log("PlayerInputRotationState.Enter()");
        }

        public void Update(float deltaTime)
        {
            Ray ray = Camera.main.ScreenPointToRay(_inputService.ScreenPosition);

            if (GroundPlane.Raycast(ray, out var distance))
            {
                var worldPoint = ray.GetPoint(distance);
                _rotationDirection.Value = (worldPoint - _transform.position).normalized;
            }
        }

        public override void Exit()
        {
            base.Exit();
            
            //Debug.Log("PlayerInputRotationState.Exit()");
        }
    }
}
