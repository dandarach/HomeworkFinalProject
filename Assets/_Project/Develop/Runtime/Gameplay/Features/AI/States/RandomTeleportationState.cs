using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using Assets._Project.Develop.Runtime.Utilities.StateMachineCore;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.AI.States
{
    public class RandomTeleportationState : State, IUpdatableState
    {
        private readonly ReactiveEvent _startTeleportationRequest;
        private readonly ICompositeCondition _startTeleportationCondition;

        private float _cooldownBetweenTeleportation;
        private float _time;

        public RandomTeleportationState(
            Entity entity,
            float cooldownBetweenTeleportation)
        {
            _startTeleportationRequest = entity.StartTeleportationRequest;
            _startTeleportationCondition = entity.CanTeleport;

            _cooldownBetweenTeleportation = cooldownBetweenTeleportation;
        }

        public override void Enter()
        {
            base.Enter();

            _time = 0;
        }

        public void Update(float deltaTime)
        {
            _time += deltaTime;

            if (_time >= _cooldownBetweenTeleportation && _startTeleportationCondition.Evaluate())
            {
                _startTeleportationRequest?.Invoke();
                _time = 0;
            }
        }
    }
}
