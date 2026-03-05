using Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeature;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.EntitiesCore
{
    public class EntitiesFactory
    {
        private readonly DIContainer _container;

        public EntitiesFactory(DIContainer container)
        {
            _container = container;
        }

        public Entity CreateTestEntity()
        {
            Entity entity = CreateEmpty();

            entity
                .AddComponent(new MoveDirection() { Value = new ReactiveVariable<Vector3>(Vector3.forward) })
                .AddComponent(new MoveSpeed() { Value = new ReactiveVariable<float>(10f) });

            return entity;
        }

        private Entity CreateEmpty() => new Entity();
    }
}
