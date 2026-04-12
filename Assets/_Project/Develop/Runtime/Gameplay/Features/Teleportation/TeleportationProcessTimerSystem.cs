using System;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Teleportation
{
    public class TeleportationProcessTimerSystem : IInitializableSystem, IUpdatableSystem, IDisposableSystem
    {
        private ReactiveVariable<float> _currentTime;
        private ReactiveVariable<bool> _inTeleportationProcess;
        private ReactiveEvent _startTeleportationEvent;

        private IDisposable _startTeleportationEventDisposable;

        public void OnInit(Entity entity)
        {
            _currentTime = entity.TeleportationProcessCurrentTime;
            _inTeleportationProcess = entity.InTeleportationProcess;
            _startTeleportationEvent = entity.StartTeleportationEvent;

            _startTeleportationEventDisposable = _startTeleportationEvent.Subscribe(OnStartTeleportationProcess);
        }

        private void OnStartTeleportationProcess()
        {
            _currentTime.Value = 0;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_inTeleportationProcess.Value == false)
                return;

            _currentTime.Value += deltaTime;
        }

        public void OnDispose()
        {
            _startTeleportationEventDisposable.Dispose();
        }
    }
}
