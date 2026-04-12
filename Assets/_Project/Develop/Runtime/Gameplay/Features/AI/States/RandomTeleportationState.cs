using System;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.Features.Teleportation;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using Assets._Project.Develop.Runtime.Utilities.StateMachineCore;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.AI.States
{
    public class RandomTeleportationState : State, IUpdatableState
    {
        private readonly ReactiveEvent _startTeleportationRequest;
        private readonly float _teleportationRadius;
        
        private ReactiveVariable<Vector3> _randomPosition;

        public RandomTeleportationState(
            Entity entity,
            float teleportationRadius)
        {
            _startTeleportationRequest = entity.StartTeleportationRequest;
            _randomPosition = entity.RandomTeleportationPosition;

            _teleportationRadius = teleportationRadius;
        }

        public override void Enter()
        {
            base.Enter();

            Debug.Log($"RandomTeleportationState.Enter()");
            UpdateRandomPosition();
            _startTeleportationRequest?.Invoke();
        }

        public override void Exit()
        {
            base.Exit();

            Debug.Log($"RandomTeleportationState.Exit()");
        }

        public void Update(float deltaTime)
        {
        }

        private void UpdateRandomPosition()
        {
            Vector3 randomPoint = UnityEngine.Random.insideUnitSphere * _teleportationRadius;
            _randomPosition.Value = new Vector3(randomPoint.x, 0, randomPoint.y);

            Debug.Log($"UpdateRandomPosition: {_randomPosition.Value}");
        }
    }
}
