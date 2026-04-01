using System;
using System.Collections.Generic;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Gameplay.Features.Teleportation;
using Assets._Project.Develop.Runtime.Utilities;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.ApplyDamage
{
    public class DealDamageOnDistanceSystem : IInitializableSystem, IDisposableSystem
    {
        private Buffer<Entity> _contacts;
        private ReactiveVariable<float> _damage;
        private ReactiveEvent _endTeleportationEvent;

        private IDisposable _endTeleportationDisposable;

        public void OnInit(Entity entity)
        {
            _contacts = entity.ContactEntitiesBuffer;
            _damage = entity.DistanceDamage;
            _endTeleportationEvent = entity.EndTeleportationEvent;

            _endTeleportationDisposable = _endTeleportationEvent.Subscribe(ApplyDamage);
        }

        public void OnDispose()
        {
            _endTeleportationDisposable.Dispose();
        }

        private void ApplyDamage()
        {
            Debug.LogWarning($"DealDamageOnDistanceSystem");

            for (int i = 0;  i < _contacts.Count; i++)
            {
                Entity contactEntity = _contacts.Items[i];

                if (contactEntity.HasComponent<TakeDamageRequest>())
                {
                    Debug.LogWarning($"DealDamageOnDistanceSystem. Apply {_damage.Value} damage to {contactEntity.ID}");
                    contactEntity.TakeDamageRequest.Invoke(_damage.Value);
                }
            }
        }
    }
}
