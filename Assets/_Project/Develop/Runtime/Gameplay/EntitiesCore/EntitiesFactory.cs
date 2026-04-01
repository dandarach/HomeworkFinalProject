using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using Assets._Project.Develop.Runtime.Gameplay.Features.ApplyDamage;
using Assets._Project.Develop.Runtime.Gameplay.Features.Attack;
using Assets._Project.Develop.Runtime.Gameplay.Features.Attack.Shoot;
using Assets._Project.Develop.Runtime.Gameplay.Features.Energy;
using Assets._Project.Develop.Runtime.Gameplay.Features.LyfeCycle;
using Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeature;
using Assets._Project.Develop.Runtime.Gameplay.Features.Sensors;
using Assets._Project.Develop.Runtime.Gameplay.Features.Teleportation;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities;
using Assets._Project.Develop.Runtime.Utilities.Conditions;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.EntitiesCore
{
    public class EntitiesFactory
    {
        private readonly DIContainer _container;
        private readonly EntitiesLifeContext _entitiesLifeContext;
        private readonly CollidersRegistryService _collidersRegistryService;
        private readonly MonoEntitiesFactory _monoEntitiesFactory;

        public EntitiesFactory(DIContainer container)
        {
            _container = container;
            _entitiesLifeContext = _container.Resolve<EntitiesLifeContext>();
            _collidersRegistryService = _container.Resolve<CollidersRegistryService>();
            _monoEntitiesFactory = _container.Resolve<MonoEntitiesFactory>();
        }

        public Entity CreateHero(Vector3 position)
        {
            Entity entity = CreateEmpty();

            _monoEntitiesFactory.Create(entity, position, "Entities/Hero");

            entity
                .AddMaxHealth(new ReactiveVariable<float>(100f))
                .AddCurrentHealth(new ReactiveVariable<float>(100f))
                .AddIsDead()
                .AddInDeathProcess()
                .AddTakeDamageRequest()
                .AddTakeDamageEvent()

                //.AddContactsDetectingMask(1 << LayerMask.NameToLayer("Characters"))
                //.AddContactCollidersBuffer(new Buffer<Collider>(64))
                //.AddContactEntitiesBuffer(new Buffer<Entity>(64))
                //.AddBodyContactDamage(new ReactiveVariable<float>(50))

                .AddCurrentEnergy(new ReactiveVariable<float>(100f))
                .AddMaxEnergy(new ReactiveVariable<float>(100f))
                .AddEnergyRefillInitialTime(new ReactiveVariable<float>(5f))
                .AddEnergyRefillCurrentTime()
                .AddTeleportationRadius(new ReactiveVariable<float>(5f))
                .AddRandomTeleportationPosition()
                .AddRequiredEnergyForTeleportation(new ReactiveVariable<float>(25f))
                .AddTeleportationProcessInitialTime(new ReactiveVariable<float>(0.5f))
                .AddTeleportationProcessCurrentTime()
                .AddInTeleportationProcess()
                .AddStartTeleportationRequest()
                .AddStartTeleportationEvent()
                .AddEndTeleportationEvent();

            ICompositeCondition mustDie = new CompositeCondition()
                .Add(new FuncCondition(() => entity.CurrentHealth.Value <= 0));

            ICompositeCondition mustSelfRelease = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == true))
                .Add(new FuncCondition(() => entity.InDeathProcess.Value == false));

            ICompositeCondition canApplyDamage = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false));

            ICompositeCondition canStartTeleport = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false))
                .Add(new FuncCondition(() => entity.CurrentEnergy.Value >= entity.RequiredEnergyForTeleportation.Value))
                .Add(new FuncCondition(() => entity.InTeleportationProcess.Value == false));

            ICompositeCondition canRefillEnergy = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false))
                .Add(new FuncCondition(() => entity.CurrentEnergy.Value < entity.MaxEnergy.Value))
                .Add(new FuncCondition(() => entity.InTeleportationProcess.Value == false));

            entity
                .AddMustDie(mustDie)
                .AddMustSelfRelease(mustSelfRelease)
                .AddCanApplyDamage(canApplyDamage)
                .AddCanTeleport(canStartTeleport)
                .AddCanRefillEnergy(canRefillEnergy);

            entity
                //.AddSystem(new BodyContactsDetectingSystem())
                //.AddSystem(new SphereContactsDetectingSystem())
                //.AddSystem(new BodyContactsEntitiesFilterSystem(_collidersRegistryService))
                //.AddSystem(new DealDamageOnContactSystem())

                .AddSystem(new ApplyDamageSystem())
                .AddSystem(new DeathSystem())
                .AddSystem(new DisableCollidersOnDeathSystem())
                .AddSystem(new SelfReleaseSystem(_entitiesLifeContext))
                .AddSystem(new StartTeleportationSystem())
                .AddSystem(new TeleportationProcessTimerSystem())
                .AddSystem(new EndTeleportationSystem())
                .AddSystem(new RandomMovementSystem())
                .AddSystem(new TeleportRigidbodyMovementSystem())
                .AddSystem(new EnergySystem());

            _entitiesLifeContext.Add(entity);

            return entity;
        }

        public Entity CreateGhost(Vector3 position)
        {
            Entity entity = CreateEmpty();

            _monoEntitiesFactory.Create(entity, position, "Entities/Ghost");

            entity
                .AddMaxHealth(new ReactiveVariable<float>(100f))
                .AddCurrentHealth(new ReactiveVariable<float>(100f))
                .AddIsDead()
                .AddInDeathProcess()
                .AddTakeDamageRequest()
                .AddTakeDamageEvent()

                .AddContactsDetectingMask(1 << LayerMask.NameToLayer("Characters"))
                .AddContactCollidersBuffer(new Buffer<Collider>(64))
                .AddContactEntitiesBuffer(new Buffer<Entity>(64))
                .AddBodyContactDamage(new ReactiveVariable<float>(50));

            ICompositeCondition mustDie = new CompositeCondition()
                .Add(new FuncCondition(() => entity.CurrentHealth.Value <= 0));

            ICompositeCondition mustSelfRelease = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == true))
                .Add(new FuncCondition(() => entity.InDeathProcess.Value == false));

            ICompositeCondition canApplyDamage = new CompositeCondition()
                .Add(new FuncCondition(() => entity.IsDead.Value == false));

            entity
                .AddMustDie(mustDie)
                .AddMustSelfRelease(mustSelfRelease)
                .AddCanApplyDamage(canApplyDamage);

            entity
                .AddSystem(new BodyContactsDetectingSystem())
                .AddSystem(new BodyContactsEntitiesFilterSystem(_collidersRegistryService))
                .AddSystem(new DealDamageOnContactSystem())

                .AddSystem(new ApplyDamageSystem())
                .AddSystem(new DeathSystem())
                .AddSystem(new DisableCollidersOnDeathSystem())
                .AddSystem(new SelfReleaseSystem(_entitiesLifeContext));

            _entitiesLifeContext.Add(entity);

            return entity;
        }

        private Entity CreateEmpty() => new Entity();
    }
}
