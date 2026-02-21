using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class StringValidator
    {
        public event Action<bool> OnStringValidate;

        private string _inputString;
        private string _stringToMatch;

        private bool _isRunning = false;

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

            _inputString += Input.inputString;
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
            OnStringValidate?.Invoke(result);
        }
    }
}
