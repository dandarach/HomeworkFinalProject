using System.Collections.Generic;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.AI.States
{
    public interface ITargetSelector
    {
        Entity SelectTargetFrom(IEnumerable<Entity> targets);
    }
}
