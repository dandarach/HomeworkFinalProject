using UnityEngine;

namespace Assets._Project.Develop.Runtime.Utilities.InputSystem
{
    public class InputHandler : IInput
    {
        public KeyCode InputKey => Input.GetKeyDown(_continueGameButton);
    }
}