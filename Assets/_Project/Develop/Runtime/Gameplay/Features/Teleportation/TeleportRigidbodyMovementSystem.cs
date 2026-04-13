using System;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Teleportation
{
    public class TeleportRigidbodyMovementSystem : IInitializableSystem, IDisposableSystem
    {
        private ReactiveEvent _startTeleportationEvent;
        private ReactiveEvent _endTeleportationEvent;
        private ReactiveVariable<Vector3> _randomPosition;
        private Rigidbody _rigidbody;

        private IDisposable _teleportationStartDisposable;
        private IDisposable _teleportationEndDisposable;

        public void OnInit(Entity entity)
        {
            _startTeleportationEvent = entity.StartTeleportationEvent;
            _endTeleportationEvent = entity.EndTeleportationEvent;
            _rigidbody = entity.Rigidbody;
            _randomPosition = entity.TeleportationPosition;

            _teleportationStartDisposable = _startTeleportationEvent.Subscribe(OnTeleportStart);
            _teleportationEndDisposable = _endTeleportationEvent.Subscribe(OnTeleportEnd);
        }

        private void OnTeleportStart()
        {
            _rigidbody.gameObject.SetActive(false);
        }

        private void OnTeleportEnd()
        {
            //Debug.Log($"Teleported to {_randomPosition.Value}");

            _rigidbody.gameObject.SetActive(true);
            _rigidbody.transform.position = _randomPosition.Value;
        }

        public void OnDispose()
        {
            _teleportationStartDisposable.Dispose();
            _teleportationEndDisposable.Dispose();
        }
    }
}
