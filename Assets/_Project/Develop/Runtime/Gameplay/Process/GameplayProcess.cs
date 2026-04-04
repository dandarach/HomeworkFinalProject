using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Process
{
    public class GameplayProcess : IDisposable
    {
        public event Action OnWin;
        public event Action OnDefeat;

        public GameplayProcess()
        {
        }

        public void Run(string symbols, int symbolsToGuess)
        {
        }

        public void Update()
        {
        }

        public void Dispose()
        {

        }

        private void OnStringValidationEnd(bool validationResult, string enteredString)
        {
            if (validationResult)
                OnWin?.Invoke();
            else
                OnDefeat?.Invoke();
        }
    }
}
