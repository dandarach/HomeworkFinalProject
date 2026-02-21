using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class StringChecker
    {
        private string _inputString;
        private string _stringToMatch;

        public void Initialize(string stringToMatch)
        {
            _stringToMatch = stringToMatch;
            _inputString = "";
        }

        public void Check()
        {
            _inputString += Input.inputString;
            Debug.Log(_inputString);
        }
    }
}
