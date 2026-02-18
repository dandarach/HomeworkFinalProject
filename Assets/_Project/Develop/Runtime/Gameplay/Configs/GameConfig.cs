using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Configs
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/GameConfig", fileName = "GameConfig")]
    public class GameConfig : ScriptableObject
    {
        [field: SerializeField] public string NumbersList { get; private set; }
        [field: SerializeField] public string LettersList { get; private set; }
        [field: SerializeField] public int SymbolsCount { get; private set; }
    }
}
