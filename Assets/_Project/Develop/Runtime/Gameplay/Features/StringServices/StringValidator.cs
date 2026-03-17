using System;
using Assets._Project.Develop.Runtime.Gameplay.InputSystem;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.StringServices
{
    public class StringValidator
    {
        public event Action<bool, string> OnStringValidate;

        private string _stringToMatch;
        private IGameplayInput _input;
        private bool _isRunning = false;

        public StringValidator(IGameplayInput input)
        {
            _input = input;
            InputString = new ReactiveVariable<string>();
        }
        
        public ReactiveVariable<string> InputString { get; private set; }
        
        public void Run(string stringToMatch)
        {
            _stringToMatch = stringToMatch;
            InputString.Value = "";
            _isRunning = true;
        }

        public void Update()
        {
            if (_isRunning == false)
                return;

            InputString.Value += _input.SelectedSymbol;
            Check();
        }

        private void Check()
        {
            if (InputString.Value == _stringToMatch)
                OnValidationEnd(true);

            if (InputString.Value.Length == _stringToMatch.Length)
                OnValidationEnd(false);
        }

        private void OnValidationEnd(bool result)
        {
            _isRunning = false;
            OnStringValidate?.Invoke(result, InputString.Value);
        }
    }
}
