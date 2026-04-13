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
        private string _id = "";

        public void OnInit(Entity entity)
        {
            _startTeleportationRequest = entity.StartTeleportationRequest;
            _startTeleportationEvent = entity.StartTeleportationEvent;
            _inTeleportationProcess = entity.InTeleportationProcess;
            _canStartTeleportation = entity.CanTeleport;
            _id = entity.ID;

            _teleportationRequestDispose = _startTeleportationRequest.Subscribe(OnTeleportationRequest);
        }

        private void OnTeleportationRequest()
        {
            if (_canStartTeleportation.Evaluate())
            {
                _inTeleportationProcess.Value = true;
                _startTeleportationEvent.Invoke();
                Debug.Log($"{_id} Teleportation started");
            }
            else
            {
                Debug.LogWarning($"{_id} Cannot teleport");
            }
        }

        public void OnDispose()
        {
            _teleportationRequestDispose.Dispose();
        }
    }
}
