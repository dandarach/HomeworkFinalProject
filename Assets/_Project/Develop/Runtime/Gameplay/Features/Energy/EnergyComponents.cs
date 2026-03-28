using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.Reactive;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Energy
{
    public class CurrentEnergy : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class MaxEnergy : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class EnergyRefillValue : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class EnergyRefillInitialTime : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class EnergyRefillCurrentTime : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }

    public class CanRefillEnergy : IEntityComponent
    {
        public ICompositeCondition Value;
    }
}
