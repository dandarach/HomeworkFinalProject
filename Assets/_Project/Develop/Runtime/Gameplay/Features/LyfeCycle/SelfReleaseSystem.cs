using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Systems;
using Assets._Project.Develop.Runtime.Utilities.Reactive;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.LyfeCycle
{
    public class SelfReleaseSystem : IInitializableSystem, IUpdatableSystem
    {
        private readonly EntitiesLifeContext _entitiesLifeContext;

        private Entity _entity;
        
        private ReactiveVariable<bool> _isDead;
        private ReactiveVariable<bool> _inDeathProcess;

        public SelfReleaseSystem(EntitiesLifeContext entitiesLifeContext)
        {
            _entitiesLifeContext = entitiesLifeContext;
        }

        public void OnInit(Entity entity)
        {
            _entity = entity;
            _isDead = entity.IsDead;
            _inDeathProcess = entity.InDeathProcess;
        }

        public void OnUpdate(float deltaTime)
        {
            if (_isDead.Value && _inDeathProcess.Value == false)
                _entitiesLifeContext.Release(_entity);
        }
    }
}
