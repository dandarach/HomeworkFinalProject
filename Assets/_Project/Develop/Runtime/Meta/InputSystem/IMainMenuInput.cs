using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.InputSystem
{
    public interface IMainMenuInput
    {
        bool DigitsGameModeSelected { get; }
        bool LettersGameModeSelected { get; }
    }
}
