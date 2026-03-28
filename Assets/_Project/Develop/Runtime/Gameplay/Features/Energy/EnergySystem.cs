using System;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Gameplay.Features.Teleportation;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Energy
{
    public class EnergySystem : IInitializableSystem, IDisposableSystem
    {
        private ReactiveEvent _endTeleportationEvent;
        private ReactiveVariable<float> _requiredEnergyForTeleportation;
        private ReactiveVariable<float> _currentEnergy;
        private ReactiveVariable<float> _maxEnergy;
        private ReactiveVariable<float> _refillCurrentTime;
        private ReactiveVariable<float> _refillInitialTime;
        private ICompositeCondition _canRefillEnergy;

        private IDisposable _teleportationEndDisposable;

        public void OnInit(Entity entity)
        {
            _endTeleportationEvent = entity.EndTeleportationEvent;
            _requiredEnergyForTeleportation = entity.RequiredEnergyForTeleportation;
            _currentEnergy = entity.CurrentEnergy;
            _maxEnergy = entity.MaxEnergy;
            _refillCurrentTime = entity.EnergyRefillCurrentTime;
            _refillInitialTime = entity.EnergyRefillInitialTime;
            _canRefillEnergy = entity.CanRefillEnergy;

            _teleportationEndDisposable = _endTeleportationEvent.Subscribe(OnTeleportEnd);
        }

        private void OnTeleportEnd()
        {
            _currentEnergy.Value -= _requiredEnergyForTeleportation.Value;
            
            if (_currentEnergy.Value < 0)
                _currentEnergy.Value = 0;

            Debug.Log($"Spend {_requiredEnergyForTeleportation.Value} energy. Current energy: {_currentEnergy.Value}");
        }

        public void OnDispose()
        {
            _teleportationEndDisposable.Dispose();
        }
    }
}
