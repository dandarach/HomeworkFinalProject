using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure.MainMenu.InputSystem
{
    public class MainMenuInputHandler : IMainMenuInput
    {
        private KeyCode _digitsGameModeKey;
        private KeyCode _lettersGameModeKey;

        public MainMenuInputHandler(KeyCode digitsGameModeKey, KeyCode lettersGameModeKey)
        {
            _digitsGameModeKey = digitsGameModeKey;
            _lettersGameModeKey = lettersGameModeKey;
        }

        public bool DigitsGameModeSelected => Input.GetKeyDown(_digitsGameModeKey);

        public bool LettersGameModeSelected => Input.GetKeyDown(_lettersGameModeKey);
    }
}