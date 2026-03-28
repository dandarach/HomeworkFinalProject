using System;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.InputSystem
{
    public class GameplayInput : IGameplayInput
    {
        public string SelectedSymbol => Input.inputString;
    }
}
