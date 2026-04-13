using System;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Energy
{
    public class EnergySystem : IInitializableSystem, IDisposableSystem, IUpdatableSystem
    {
        private ReactiveEvent _endTeleportationEvent;
        private ReactiveVariable<float> _requiredEnergyForTeleportation;
        private ReactiveVariable<float> _currentEnergy;
        private ReactiveVariable<float> _maxEnergy;
        private ReactiveVariable<float> _refillCurrentTime;
        private ReactiveVariable<float> _refillInitialTime;
        private ICompositeCondition _canRefillEnergy;

        private IDisposable _teleportationEndDisposable;
        private string _id;

        public void OnInit(Entity entity)
        {
            _endTeleportationEvent = entity.EndTeleportationEvent;
            _requiredEnergyForTeleportation = entity.RequiredEnergyForTeleportation;
            _currentEnergy = entity.CurrentEnergy;
            _maxEnergy = entity.MaxEnergy;
            _refillCurrentTime = entity.EnergyRefillCurrentTime;
            _refillInitialTime = entity.EnergyRefillInitialTime;
            _canRefillEnergy = entity.CanRefillEnergy;
            _id = entity.ID;

            _refillCurrentTime.Value = _refillInitialTime.Value;

            _teleportationEndDisposable = _endTeleportationEvent.Subscribe(WasteEnerge);
        }

        public void OnUpdate(float deltaTime)
        {
            if (_canRefillEnergy.Evaluate() == false)
                return;

            _refillCurrentTime.Value -= deltaTime;

            if (IsTimeOver())
                RefillEnergy();
        }

        private void WasteEnerge()
        {
            _currentEnergy.Value -= _requiredEnergyForTeleportation.Value;
            _currentEnergy.Value = Mathf.Max(_currentEnergy.Value, 0);

            Debug.Log($"{_id} Wasted {_requiredEnergyForTeleportation.Value} energy. Current energy: {_currentEnergy.Value}");
        }

        private void RefillEnergy()
        {
            _currentEnergy.Value += _maxEnergy.Value / 10;
            _currentEnergy.Value = Mathf.Min(_currentEnergy.Value, _maxEnergy.Value);

            Debug.Log($"{_id} Refilled {_maxEnergy.Value / 10} energy. Current energy: {_currentEnergy.Value}");
            
            _refillCurrentTime.Value = _refillInitialTime.Value;
        }

        private bool IsTimeOver() => _refillCurrentTime.Value <= 0;

        public void OnDispose()
        {
            _teleportationEndDisposable.Dispose();
        }
    }
}
