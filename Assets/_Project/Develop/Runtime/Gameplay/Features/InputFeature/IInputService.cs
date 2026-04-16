using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.InputFeature
{
    public interface IInputService
    {
        bool IsEnabled { get; set; }

        Vector3 Direction { get; }

        Vector3 ScreenPosition { get; }

        bool IsFireButtonDown { get; }
        
        bool IsFireButtonUp { get; }
    }
}
