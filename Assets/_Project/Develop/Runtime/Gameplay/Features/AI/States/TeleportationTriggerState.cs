using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using Assets._Project.Develop.Runtime.Utilities.StateMachineCore;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.AI.States
{
    public class TeleportationTriggerState : State, IUpdatableState
    {
        private ReactiveEvent _teleportationRequest;

        public TeleportationTriggerState(Entity entity)
        {
            _teleportationRequest = entity.StartTeleportationRequest;
        }

        public override void Enter()
        {
            base.Enter();

            _teleportationRequest.Invoke();
        }

        public void Update(float deltaTime)
        {
        }
    }
}
