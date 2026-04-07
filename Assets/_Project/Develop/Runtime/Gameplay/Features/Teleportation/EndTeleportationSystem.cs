using System;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Teleportation
{
    public class EndTeleportationSystem : IInitializableSystem, IDisposableSystem
    {
        private ReactiveEvent _endTeleportationEvent;
        private ReactiveVariable<bool> _inTeleportationProcess;
        private ReactiveVariable<float> _teleportationProcessInitialTime;
        private ReactiveVariable<float> _teleportationProcessCurrentTime;

        private IDisposable _timerDisposable;

        public void OnInit(Entity entity)
        {
            _endTeleportationEvent = entity.EndTeleportationEvent;
            _inTeleportationProcess = entity.InTeleportationProcess;
            _teleportationProcessInitialTime = entity.TeleportationProcessInitialTime;
            _teleportationProcessCurrentTime = entity.TeleportationProcessCurrentTime;

            _timerDisposable = _teleportationProcessCurrentTime.Subscribe(OnTimerChanged);
        }

        private void OnTimerChanged(float arg1, float currentTime)
        {
            if (IsTimerDone(currentTime))
            {
                _inTeleportationProcess.Value = false;
                _endTeleportationEvent.Invoke();
                //Debug.Log("Teleportation ended");
            }
        }

        public void OnDispose()
        {
            _timerDisposable.Dispose();
        }

        private bool IsTimerDone(float currentTime) => currentTime >= _teleportationProcessInitialTime.Value;
    }
}
