using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.Reactive;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Teleportation
{
    public class RequiredEnergyForTeleportation : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class TeleportationRadius : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }
    
    public class CanTeleport : IEntityComponent
    {
        public ICompositeCondition Value;
    }
    
    public class TeleportationProcessInitialTime : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class TeleportationProcessCurrentTime : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class StartTeleportationRequest : IEntityComponent
    {
        public ReactiveEvent Value;
    }

    public class StartTeleportationEvent : IEntityComponent
    {
        public ReactiveEvent Value;
    }

    public class InTeleportationProcess : IEntityComponent
    {
        public ReactiveVariable<bool> Value;
    }

    public class EndTeleportationEvent : IEntityComponent
    {
        public ReactiveEvent Value;
    }
}
