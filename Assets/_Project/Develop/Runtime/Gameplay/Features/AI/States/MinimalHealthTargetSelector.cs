using System.Collections.Generic;
using System.Linq;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.Features.ApplyDamage;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.AI.States
{
    public class MinimalHealthTargetSelector : ITargetSelector
    {
        private Entity _source;

        public MinimalHealthTargetSelector(Entity entity)
        {
            _source = entity;
        }

        public Entity SelectTargetFrom(IEnumerable<Entity> targets)
        {
            IEnumerable<Entity> selectedTargets = targets.Where(target =>
            {
                bool result = target.HasComponent<TakeDamageRequest>();

                if (target.TryGetCanApplyDamage(out ICompositeCondition canAppyDamage))
                {
                    result = result && canAppyDamage.Evaluate();
                }

                result = result && target != _source;

                return result;
            });

            if (selectedTargets.Any() == false)
                return null;

            Entity closestTarget = selectedTargets.First();
            float minHealth = GetHeath(closestTarget);

            foreach (Entity target in selectedTargets)
            {
                float health = GetHeath(target);

                if (health < minHealth)
                {
                    minHealth = health;
                    closestTarget = target;
                }
            }
            
            return closestTarget;
        }

        private float GetHeath(Entity target) => target.CurrentHealth.Value;
    }
}
