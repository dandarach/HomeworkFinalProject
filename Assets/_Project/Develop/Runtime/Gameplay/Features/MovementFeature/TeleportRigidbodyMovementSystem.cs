using System;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeature
{
    public class TeleportRigidbodyMovementSystem : IInitializableSystem, IDisposableSystem
    {
        private ReactiveEvent _startTeleportationEvent;
        private ReactiveEvent _endTeleportationEvent;
        private ReactiveVariable<Vector3> _randomPosition;
        private Rigidbody _rigidbody;
        private ICompositeCondition _canTeleport;

        private IDisposable _teleportationStartDisposable;
        private IDisposable _teleportationEndDisposable;

        public void OnInit(Entity entity)
        {
            _startTeleportationEvent = entity.StartTeleportationEvent;
            _endTeleportationEvent = entity.EndTeleportationEvent;
            _rigidbody = entity.Rigidbody;
            _canTeleport = entity.CanTeleport;

            _teleportationStartDisposable = _startTeleportationEvent.Subscribe(OnTeleportStart);
            _teleportationEndDisposable = _endTeleportationEvent.Subscribe(OnTeleportEnd);
        }

        private void OnTeleportStart()
        {
            Debug.Log("*** Teleportation start ***");
            _rigidbody.gameObject.SetActive(false);
        }

        private void OnTeleportEnd()
        {
            Debug.Log("*** Teleportation end ***");

            Vector3 randomPoint = UnityEngine.Random.insideUnitSphere * 2f;
            randomPoint.y = 0;
            _rigidbody.transform.position += randomPoint;

            _rigidbody.gameObject.SetActive(true);
            //_rigidbody.MovePosition(_randomPosition.Value);
        }

        public void OnDispose()
        {
            _teleportationStartDisposable.Dispose();
            _teleportationEndDisposable.Dispose();
        }
    }
}
