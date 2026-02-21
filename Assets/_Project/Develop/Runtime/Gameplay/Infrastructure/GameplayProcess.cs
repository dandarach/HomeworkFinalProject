using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayProcess : IDisposable
    {
        public event Action OnWin;
        public event Action OnDefeat;

        private StringGenerator _generator;
        private StringValidator _validator;

        public GameplayProcess(StringGenerator generator, StringValidator validator)
        {
            _generator = generator;
            _validator = validator;
        }

        public void Run(GameplayInputArgs inputArgs)
        {
            string generatedString = _generator.Generate(inputArgs.Symbols, inputArgs.SymbolsToGuess);
            Debug.LogWarning($"*** Generated string: {generatedString}");

            _validator.OnStringValidate += OnStringValidationEnd;
            _validator.Run(generatedString);
        }

        public void Update()
        {
            _validator.Update();
        }

        public void Dispose()
        {

        }

        private void OnStringValidationEnd(bool validationResult)
        {
            _validator.OnStringValidate -= OnStringValidationEnd;

            if (validationResult)
                OnWin?.Invoke();
            else
                OnDefeat?.Invoke();
        }
    }
}
