using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Utilities.Reactive;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.DistanceTakeDamage
{
    public class DistanceDamage : IEntityComponent
    {
        public ReactiveVariable<float> Value;
    }
}