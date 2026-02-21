using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Utilities.InputSystem
{
    public interface IMainMenuInput
    {
        bool DigitsGameModeSelected { get; }
        bool LettersGameModeSelected { get; }
    }
}
