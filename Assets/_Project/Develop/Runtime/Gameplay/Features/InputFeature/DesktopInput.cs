using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.InputSystem
{
    public class DesktopInput : IInputService
    {
        private readonly KeyCode _switchEntityKey = KeyCode.Space;
        private const string HorizontalAxisName = "Horizontal";
        private const string VerticalAxisName = "Vertical";

        public string SelectedSymbol => Input.inputString;

        public Vector3 Direction
            => new Vector3(Input.GetAxisRaw(HorizontalAxisName), 0, Input.GetAxisRaw(VerticalAxisName));

        public bool IsSwitchEntityButtonPressed
            => Input.GetKeyDown(_switchEntityKey);
    }
}
