using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.Features.MovementFeature;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay
{
    public class TestGameplay : MonoBehaviour
    {
        private DIContainer _container;
        private EntitiesFactory _entitiesFactory;
        private bool _isRunning;

        public void Initialize(DIContainer container)
        {
            _container = container;
            _entitiesFactory = _container.Resolve<EntitiesFactory>();
        }

        public void Run()
        {
            Entity entity = _entitiesFactory.CreateTestEntity();
            Debug.Log("Move direction: " + entity.GetComponent<MoveDirection>().Value.Value.ToString());
            Debug.Log("Move speed: " + entity.GetComponent<MoveSpeed>().Value.Value.ToString());

            _isRunning = true;
        }

        public void Update()
        {
            if (_isRunning == false)
                return;


        }
    }
}
