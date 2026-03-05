using Assets._Project.Develop.Runtime.Infrastructure.DI;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay
{
    public class TestGameplay : MonoBehaviour
    {
        private DIContainer _container;
        private bool _isRunning;

        public void Initialize(DIContainer container)
        {
            _container = container;
        }

        public void Run()
        {
            _isRunning = true;
        }

        public void Update()
        {
            if (_isRunning == false)
                return;


        }
    }
}
