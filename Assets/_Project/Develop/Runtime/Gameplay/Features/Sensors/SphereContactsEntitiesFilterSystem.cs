using System;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Sensors
{
    public class SphereContactsEntitiesFilterSystem : IInitializableSystem, IDisposableSystem
    {
        private Buffer<Collider> _contacts;
        private Buffer<Entity> _contactsEntities;
        private ReactiveEvent _endTeleportationEvent;

        private readonly CollidersRegistryService _collidersRegistryService;
        
        private IDisposable _endTeleportationDisposable;

        public SphereContactsEntitiesFilterSystem(CollidersRegistryService collidersRegistryService)
        {
            _collidersRegistryService = collidersRegistryService;
        }

        public void OnInit(Entity entity)
        {
            _contacts = entity.ContactCollidersBuffer;
            _contactsEntities = entity.ContactEntitiesBuffer;
            _endTeleportationEvent = entity.EndTeleportationEvent;

            _endTeleportationDisposable = _endTeleportationEvent.Subscribe(FilterContacts);
        }

        public void OnDispose()
        {
            _endTeleportationDisposable.Dispose();
        }

        private void FilterContacts()
        {
            _contactsEntities.Count = 0;

            for (int i = 0;  i < _contacts.Count; i++)
            {
                Collider collider = _contacts.Items[i];
                Entity contactEntity = _collidersRegistryService.GetBy(collider);

                if (contactEntity != null)
                {
                    _contactsEntities.Items[_contactsEntities.Count] = contactEntity;
                    _contactsEntities.Count++;
                }
            }
        }
    }
}
