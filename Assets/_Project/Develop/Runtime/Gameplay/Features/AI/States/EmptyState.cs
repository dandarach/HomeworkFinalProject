using Assets._Project.Develop.Runtime.Utilities.StateMachineCore;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.AI.States
{
    public class EmptyState : State, IUpdatableState
    {
        public override void Enter()
        {
            base.Enter();

            //Debug.Log($"EmptyState.Enter()");
        }

        public override void Exit()
        {
            base.Exit();

            //Debug.Log($"EmptyState.Exit()");
        }

        public void Update(float deltaTime)
        {
        }
    }
}
