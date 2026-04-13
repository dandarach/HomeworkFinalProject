using System;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.Features.Teleportation;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using Assets._Project.Develop.Runtime.Utilities.StateMachineCore;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.AI.States
{
    public class TeleportationToMinHealthTargetState : State, IUpdatableState
    {
        private readonly ReactiveEvent _startTeleportationRequest;
        private readonly float _teleportationRadius;

        private ReactiveVariable<Entity> _currentTarget;
        private ReactiveVariable<Vector3> _randomPosition;
        private string _id = "";

        public TeleportationToMinHealthTargetState(
            Entity entity,
            float teleportationRadius)
        {
            _startTeleportationRequest = entity.StartTeleportationRequest;
            _currentTarget = entity.CurrentTarget;
            _id = entity.ID;

            _teleportationRadius = teleportationRadius;
        }

        public override void Enter()
        {
            base.Enter();

            Debug.Log($"{_id} TeleportationToMinHealthTargetState.Enter()");
            UpdatePosition();
            _startTeleportationRequest?.Invoke();
        }

        public override void Exit()
        {
            base.Exit();

            Debug.Log($"{_id} TeleportationToMinHealthTargetState.Exit()");
        }

        public void Update(float deltaTime)
        {
        }

        private void UpdatePosition()
        {
            Vector3 randomPoint = UnityEngine.Random.insideUnitSphere * _teleportationRadius;
            _randomPosition.Value = new Vector3(randomPoint.x, 0, randomPoint.y);
        }
    }
}
