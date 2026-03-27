using System;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.Attack
{
    public class AttackCooldownTimerSystem : IInitializableSystem, IUpdatableSystem, IDisposableSystem
    {
        private ReactiveVariable<float> _initialTime;
        private ReactiveVariable<float> _currentTime;
        private ReactiveVariable<bool> _inAttackCooldown;
        private ReactiveEvent _endAttackEvent;

        private IDisposable _endAttackEventDisposable;

        public void OnInit(Entity entity)
        {
            _initialTime = entity.AttackCooldownInitialTime;
            _currentTime = entity.AttackCooldownCurrentTime;
            _inAttackCooldown = entity.InAttackCooldown;
            _endAttackEvent = entity.EndAttackEvent;

            _endAttackEventDisposable = _endAttackEvent.Subscribe(OnEndAttack);
        }

        private void OnEndAttack()
        {
            Debug.Log("Cooldown started");
            _currentTime.Value = _initialTime.Value;
            _inAttackCooldown.Value = true;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_inAttackCooldown.Value == false)
                return;

            _currentTime.Value -= deltaTime;

            if (IsCooldownOver())
            {
                _inAttackCooldown.Value = false;
                Debug.Log("Cooldown ended");
            }
        }

        private bool IsCooldownOver() => _currentTime.Value <= 0;

        public void OnDispose()
        {
            _endAttackEventDisposable.Dispose();
        }
    }
}
