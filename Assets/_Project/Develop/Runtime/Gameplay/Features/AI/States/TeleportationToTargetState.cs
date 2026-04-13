using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using Assets._Project.Develop.Runtime.Utilities.StateMachineCore;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.AI.States
{
    public class TeleportationToTargetState : State, IUpdatableState
    {
        private readonly ReactiveEvent _startTeleportationRequest;
        private readonly float _teleportationRadius;

        private ReactiveVariable<Entity> _currentTarget;
        private ReactiveVariable<Vector3> _position;
        private string _id = "";

        public TeleportationToTargetState(
            Entity entity,
            float teleportationRadius)
        {
            _startTeleportationRequest = entity.StartTeleportationRequest;
            _currentTarget = entity.CurrentTarget;
            _position = entity.TeleportationPosition;
            _id = entity.ID;

            _teleportationRadius = teleportationRadius;
        }

        public override void Enter()
        {
            base.Enter();

            Debug.Log($"{_id} TeleportationToTargetState.Enter()");
            UpdatePosition();
            _startTeleportationRequest?.Invoke();
        }

        public override void Exit()
        {
            base.Exit();

            Debug.Log($"{_id} TeleportationToTargetState.Exit()");
        }

        public void Update(float deltaTime)
        {
        }

        private void UpdatePosition()
        {
            Entity target = _currentTarget.Value;
            _position.Value = CalculateTeleportPoint(_position.Value, target.Transform.position, _teleportationRadius);
            Debug.Log($"{_id} TeleportationToTargetState.UpdatePosition: {_position.Value}");
        }

        private Vector3 CalculateTeleportPoint(Vector3 position, Vector3 targetPosition, float teleportationRadius)
        {
            Vector3 direction = targetPosition - position;
            float distance = direction.magnitude;

            if (distance <= teleportationRadius)
                return targetPosition;
            else
                return position + direction.normalized * teleportationRadius;
        }
    }
}
