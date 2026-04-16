using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.Features.InputFeature;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using Assets._Project.Develop.Runtime.Utilities.StateMachineCore;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.AI.States
{
    public class AutoShootState : State, IUpdatableState
    {
        private ReactiveEvent _attackRequest;

        public AutoShootState(Entity entity)
        {
            _attackRequest = entity.StartAttackRequest;
        }

        public override void Enter()
        {
            //Debug.Log("AutoShootState.Enter()");
        }

        public void Update(float deltaTime)
        {
            _attackRequest.Invoke();
        }

        public override void Exit()
        {
            base.Exit();

            //Debug.Log("AutoShootState.Exit()");
        }
    }
}
