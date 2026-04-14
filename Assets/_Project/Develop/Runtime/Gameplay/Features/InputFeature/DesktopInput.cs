using System.Threading;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.InputFeature
{
    public class DesktopInput : IInputService
    {
        private const string HorizontalAxisName = "Horizontal";
        private const string VerticalAxisName = "Vertical";
        private const string MouseX = "Mouse X";
        
        private const int LeftMouseButton = 0;
        private const float Sensitivity = 2.5f;

        public bool IsEnabled { get; set; } = true;

        public Vector3 Direction
        {
            get
            {
                if (IsEnabled == false)
                    return Vector3.zero;

                return new Vector3(Input.GetAxisRaw(HorizontalAxisName), 0, Input.GetAxisRaw(VerticalAxisName));
            }
        }

        public float XAxis
        {
            get
            {
                if (IsEnabled == false)
                    return 0;

                return Input.GetAxisRaw(MouseX) * Sensitivity;
            }
        }
        
        public bool IsFireButtonPressed
        {
            get
            {
                if (IsEnabled == false)
                    return false;

                Debug.Log("LeftMouseButton");
                return Input.GetMouseButton(LeftMouseButton);
            }
        }
    }
}
