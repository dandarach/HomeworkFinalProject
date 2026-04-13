using System;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeature
{
    public class RandomMovementSystem : IInitializableSystem, IDisposableSystem
    {
        private ReactiveEvent _startTeleportationEvent;
        private ReactiveVariable<Vector3> _randomPosition;
        private ReactiveVariable<float> _teleportationRadius;

        private IDisposable _teleportationStartDisposable;

        public void OnInit(Entity entity)
        {
            _startTeleportationEvent = entity.StartTeleportationEvent;
            _randomPosition = entity.TeleportationPosition;
            _teleportationRadius = entity.TeleportationRadius;
            _teleportationStartDisposable = _startTeleportationEvent.Subscribe(OnTeleportStart);
        }

        private void OnTeleportStart()
        {
            Vector3 randomPoint = UnityEngine.Random.insideUnitSphere * _teleportationRadius.Value;
            _randomPosition.Value = new Vector3(randomPoint.x, 0, randomPoint.y);
        }

        public void OnDispose()
        {
            _teleportationStartDisposable.Dispose();
        }
    }
}
