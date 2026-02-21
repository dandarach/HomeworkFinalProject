using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayProcess : IDisposable
    {
        public event Action OnWin;
        public event Action OnDefeat;

        private StringGenerator _generator;
        private StringChecker _checker;
        private GameManager _manager;

        public GameplayProcess(StringGenerator generator, StringChecker checker, GameManager manager)
        {
            _generator = generator;
            _checker = checker;
            _manager = manager;
        }

        public void Run()
        {
            string generatedString = _generator.Generate();
            Debug.LogWarning($"*** Generated string: {generatedString}");

            _checker.Initialize(generatedString);
        }

        public void Update()
        {
            _checker.Check();
        }

        public void Dispose()
        {

        }
    }
}
