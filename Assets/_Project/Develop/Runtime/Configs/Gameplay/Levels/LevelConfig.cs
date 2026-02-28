using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs.Gameplay.Levels
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/LevelConfig", fileName = "LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        [field: SerializeField] public string Symbols { get; private set; }
        [field: SerializeField] public int SymbolsToGuess { get; private set; }
        [field: SerializeField] public int WinGameBonus { get; private set; }
        [field: SerializeField] public int LoseGameFine { get; private set; }
        [field: SerializeField] public int ResetProgressCost { get; private set; }
        [field: SerializeField] public KeyCode RestartGameKey { get; private set; }
    }
}
