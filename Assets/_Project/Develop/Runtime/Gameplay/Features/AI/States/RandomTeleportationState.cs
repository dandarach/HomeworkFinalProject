using System;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.Features.Teleportation;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using Assets._Project.Develop.Runtime.Utilities.StateMachineCore;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.AI.States
{
    public class RandomTeleportationState : State, IUpdatableState
    {
        private readonly ReactiveEvent _startTeleportationRequest;
        private readonly float _teleportationRadius;
        
        private ReactiveVariable<Vector3> _randomPosition;
        private ReactiveVariable<Vector3> _rotationDirection;
        private string _id = "";

        public RandomTeleportationState(
            Entity entity,
            float teleportationRadius)
        {
            _startTeleportationRequest = entity.StartTeleportationRequest;
            _randomPosition = entity.TeleportationPosition;
            _rotationDirection = entity.RotationDirection;
            _id = entity.ID;

            _teleportationRadius = teleportationRadius;
        }

        public override void Enter()
        {
            base.Enter();

            //Debug.Log($"{_id} RandomTeleportationState.Enter()");
            UpdateRandomPosition();
            _startTeleportationRequest?.Invoke();
        }

        public override void Exit()
        {
            base.Exit();

            //Debug.Log($"{_id} RandomTeleportationState.Exit()");
        }

        public void Update(float deltaTime)
        {
        }

        private void UpdateRandomPosition()
        {
            Vector3 oldPosition = _randomPosition.Value;
            Vector3 randomPoint = UnityEngine.Random.insideUnitSphere * _teleportationRadius;

            _randomPosition.Value = new Vector3(randomPoint.x, 0, randomPoint.y);
            _rotationDirection.Value = (_randomPosition.Value - oldPosition).normalized;
        }
    }
}
