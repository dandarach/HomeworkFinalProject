using System;
using UnityEngine;
using Assets._Project.Develop.Runtime.Gameplay.Features.StringServices;

namespace Assets._Project.Develop.Runtime.Gameplay.Process
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

        public void Run(string symbols, int symbolsToGuess)
        {
            _generator.Generate(symbols, symbolsToGuess);
            Debug.LogWarning($"*** Generated string: {_generator.GeneratedString.Value}");

            _validator.OnStringValidate += OnStringValidationEnd;
            _validator.Run(_generator.GeneratedString.Value);
        }

        public void Update()
        {
            _validator.Update();
        }

        public void Dispose()
        {

        }

        private void OnStringValidationEnd(bool validationResult, string enteredString)
        {
            _validator.OnStringValidate -= OnStringValidationEnd;
            Debug.Log($"You entered: {enteredString}");

            if (validationResult)
                OnWin?.Invoke();
            else
                OnDefeat?.Invoke();
        }
    }
}
