using Assets._Project.Develop.Runtime.Gameplay.Common;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeature;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.EntitiesCore
{
    public partial class Entity
    {
        public Entity AddRigidbody(Rigidbody value)
        {
            return AddComponent(new RigidbodyComponent() { Value = value });
        }

        public Entity AddMoveDirection(ReactiveVariable<Vector3> value)
        {
            return AddComponent(new MoveDirection() { Value = value });
        }

        public Entity AddMoveDirection()
        {
            return AddComponent(new MoveDirection() { Value = new ReactiveVariable<Vector3>() });
        }
    }
}
