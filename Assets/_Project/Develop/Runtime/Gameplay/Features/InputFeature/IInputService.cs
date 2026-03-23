using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.InputSystem
{
    public interface IInputService
    {
        string SelectedSymbol { get; }

        Vector3 Direction { get; }

        bool IsSwitchEntityButtonPressed { get; }
    }
}
