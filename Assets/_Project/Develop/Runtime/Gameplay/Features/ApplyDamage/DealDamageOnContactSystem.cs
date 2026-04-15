using System.Collections.Generic;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Gameplay.Features.TeamsFeature;
using Assets._Project.Develop.Runtime.Utilities;
using Assets._Project.Develop.Runtime.Utilities.Reactive;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.ApplyDamage
{
    public class DealDamageOnContactSystem : IInitializableSystem, IUpdatableSystem
    {
        private Entity _entity;
        private Buffer<Entity> _contacts;
        private ReactiveVariable<float> _damage;
        private List<Entity> _processedEntities;

        public void OnInit(Entity entity)
        {
            _entity = entity;
            _contacts = entity.ContactEntitiesBuffer;
            _damage = entity.BodyContactDamage;
            
            _processedEntities = new List<Entity>(_contacts.Items.Length);
        }

        public void OnUpdate(float deltaTime)
        {
            for (int i = 0;  i < _contacts.Count; i++)
            {
                Entity contactEntity = _contacts.Items[i];

                if (_processedEntities.Contains(contactEntity) == false)
                {
                    _processedEntities.Add(contactEntity);

                   TryTakeDamageFrom(_entity, contactEntity, _damage.Value);
                }
            }

            for (int i = _processedEntities.Count - 1; i >= 0; i--)
                if (ContainInContacts(_processedEntities[i]) == false)
                    _processedEntities.RemoveAt(i);
        }

        public bool TryTakeDamageFrom(Entity source, Entity damageable, float damage)
        {
            if (damageable.TryGetTakeDamageRequest(out ReactiveEvent<float> takeDamageRequest) == false)
                return false;

            if (source.TryGetTeam(out ReactiveVariable<Teams> sourceTeam)
                && damageable.TryGetTeam(out ReactiveVariable<Teams> damageableTeam))
            {
                if (sourceTeam.Value == damageableTeam.Value)
                    return false;
            }

            takeDamageRequest.Invoke(damage);
            return true;
        }

        public bool ContainInContacts(Entity entity)
        {
            for (int i = 0; i < _contacts.Count; ++i)
                if (_contacts.Items[i] == entity)
                    return true;

            return false;
        }
    }
}
