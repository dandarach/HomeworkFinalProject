using System;
using Assets._Project.Develop.Runtime.UI.CommonViews;
using Assets._Project.Develop.Runtime.UI.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Develop.Runtime.UI.Gameplay
{
    public class GameplayScreenView : MonoBehaviour, IView
    {
        [field: SerializeField] public TextView StringGeneratorView { get; private set; }
        [field: SerializeField] public TextView StringValidatorView { get; private set; }

        private void OnEnable()
        {
        }

        private void OnDisable()
        {
        }
    }
}
