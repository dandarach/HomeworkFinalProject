using System;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Sensors
{
    public class SphereContactsDetectingSystem : IInitializableSystem, IDisposableSystem
    {
        private Buffer<Collider> _contacts;
        private LayerMask _mask;
        private CapsuleCollider _body;
        private ReactiveVariable<float> _radius;
        private ReactiveEvent _endTeleportationEvent;

        private IDisposable _endTeleportationDisposable;

        public void OnInit(Entity entity)
        {
            _contacts = entity.ContactCollidersBuffer;
            _mask = entity.ContactsDetectingMask;
            _body = entity.BodyCollider;
            _radius = entity.ContactsDetectingRadius;
            _endTeleportationEvent = entity.EndTeleportationEvent;

            _endTeleportationDisposable = _endTeleportationEvent.Subscribe(DetectContacts);
        }

        public void OnDispose()
        {
            _endTeleportationDisposable.Dispose();
        }

        private void DetectContacts()
        {
            _contacts.Count = Physics.OverlapSphereNonAlloc(
                _body.transform.position,
                _radius.Value,
                _contacts.Items,
                _mask,
                QueryTriggerInteraction.Ignore);

            RemoveSelfFromContacts();
            
            Debug.Log($"SphereContactsDetectingSystem. Contacts detected: {_contacts.Count}");
            Debug.Log($"SphereContactsDetectingSystem. _body.transform.position: {_body.transform.position}");
        }

        private void RemoveSelfFromContacts()
        {
            int indexToRemove = -1;

            for (int i = 0; i < _contacts.Count; i++)
            {
                if (_contacts.Items[i] == _body)
                {
                    indexToRemove = i;
                    break;
                }
            }

            if (indexToRemove >= 0)
            {
                for (int i = indexToRemove;  i < _contacts.Count - 1; i++)
                {
                    _contacts.Items[i] = _contacts.Items[i + 1];
                }

                _contacts.Count--;
            }
        }
    }
}
