using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using static Assets._Project.Develop.Runtime.Configs.Meta.Wallet.LevelConfig;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs.Gameplay.Levels
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/LevelConfig", fileName = "LevelConfig")]
    public partial class LevelConfig : ScriptableObject
    {
        [field: SerializeField] public string Symbols { get; private set; }
        [field: SerializeField] public int SymbolsToGuess { get; private set; }
        [field: SerializeField] public CurrencyConfig WinAward { get; private set; }
        [field: SerializeField] public CurrencyConfig LosePenalty { get; private set; }
        [field: SerializeField] public CurrencyConfig ResetProgressCost { get; private set; }
        [field: SerializeField] public KeyCode RestartGameKey { get; private set; }
    }
}
