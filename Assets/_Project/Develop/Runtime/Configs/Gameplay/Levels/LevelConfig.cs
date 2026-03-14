using Assets._Project.Develop.Runtime.Configs.Meta.Wallet;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs.Gameplay.Levels
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/LevelConfig", fileName = "LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        [field: SerializeField] public GameplayMode GameplayMode { get; private set; }
        [field: SerializeField] public string Symbols { get; private set; }
        [field: SerializeField] public int SymbolsToGuess { get; private set; }
        [field: SerializeField] public CurrencyConfig WinAward { get; private set; }
        [field: SerializeField] public CurrencyConfig LosePenalty { get; private set; }
        [field: SerializeField] public KeyCode RestartGameKey { get; private set; }
    }
}
