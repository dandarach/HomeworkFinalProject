using System;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Teleportation
{
    public class StartTeleportationSystem : IInitializableSystem, IDisposableSystem
    {
        private ReactiveEvent _startTeleportationRequest;
        private ReactiveEvent _startTeleportationEvent;
        private ReactiveVariable<bool> _inTeleportationProcess;
        private ICompositeCondition _canStartTeleportation;

        private IDisposable _teleportationRequestDispose;
        private int g = 1;

        public void OnInit(Entity entity)
        {
            _startTeleportationRequest = entity.StartTeleportationRequest;
            _startTeleportationEvent = entity.StartTeleportationEvent;
            _inTeleportationProcess = entity.InTeleportationProcess;
            _canStartTeleportation = entity.CanTeleport;

            _teleportationRequestDispose = _startTeleportationRequest.Subscribe(OnTeleportationRequest);
        }

        private void OnTeleportationRequest()
        {
            Debug.LogWarning(g);
            g++;

            if (_canStartTeleportation.Evaluate())
            {
                _inTeleportationProcess.Value = true;
                _startTeleportationEvent.Invoke();
                Debug.Log("Start teleportation");
            }
            else
            {
                Debug.Log("Cannot teleport");
            }
        }

        public void OnDispose()
        {
            _teleportationRequestDispose.Dispose();
        }
    }
}
