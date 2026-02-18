using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/MenuConfig", fileName = "MenuConfig")]
    public class MenuConfig : ScriptableObject
    {
        [field: Header("Keyboard Settings")]
        [field: SerializeField] public KeyCode SelectNumbersGameModeKey { get; private set; }
        [field: SerializeField] public KeyCode SelectLettersGameModeKey { get; private set; }
        [field: SerializeField] public KeyCode RestartGameKey { get; private set; }
    }
}
