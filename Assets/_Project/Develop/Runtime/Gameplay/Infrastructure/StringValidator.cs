using System;
using Assets._Project.Develop.Runtime.Gameplay.Infrastructure.InputSystem;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class StringValidator
    {
        public event Action<bool, string> OnStringValidate;

        private string _inputString;
        private string _stringToMatch;
        private IGameplayInput _input;
        private bool _isRunning = false;

        public StringValidator(IGameplayInput input)
        {
            _input = input;
        }

        public void Run(string stringToMatch)
        {
            _stringToMatch = stringToMatch;
            _inputString = "";
            _isRunning = true;
        }

        public void Update()
        {
            if (_isRunning == false)
                return;

            _inputString += _input.SelectedSymbol;
            Check();
        }

        private void Check()
        {
            if (_inputString == _stringToMatch)
                OnValidationEnd(true);

            if (_inputString.Length == _stringToMatch.Length)
                OnValidationEnd(false);
        }

        private void OnValidationEnd(bool result)
        {
            _isRunning = false;
            OnStringValidate?.Invoke(result, _inputString);
        }
    }
}
