using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.InputSystem
{
    public class MainMenuInputHandler : IMainMenuInput
    {
        private KeyCode _digitsGameModeKey;
        private KeyCode _lettersGameModeKey;
        private KeyCode _resetGameProgressKey;
        private KeyCode _showGameProgressKey;

        public MainMenuInputHandler(
            KeyCode digitsGameModeKey,
            KeyCode lettersGameModeKey,
            KeyCode resetGameProgressKey,
            KeyCode showGameProgressKey)
        {
            _digitsGameModeKey = digitsGameModeKey;
            _lettersGameModeKey = lettersGameModeKey;
            _resetGameProgressKey = resetGameProgressKey;
            _showGameProgressKey = showGameProgressKey;
        }

        public bool DigitsGameModeSelected => Input.GetKeyDown(_digitsGameModeKey);
        public bool LettersGameModeSelected => Input.GetKeyDown(_lettersGameModeKey);
        public bool ResetGameProgressKeyPressed => Input.GetKeyDown(_resetGameProgressKey);
        public bool ShowGameProgressKeyPressed => Input.GetKeyDown(_showGameProgressKey);
    }
}