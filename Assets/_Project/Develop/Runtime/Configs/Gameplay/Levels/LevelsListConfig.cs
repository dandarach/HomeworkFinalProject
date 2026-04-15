using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Assets._Project.Develop.Runtime.Configs.Meta.Wallet.LevelConfig;

namespace Assets._Project.Develop.Runtime.Configs.Gameplay.Levels
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/Levels/NewLevelsListConfig", fileName = "LevelsListConfig")]
    public class LevelsListConfig : ScriptableObject
    {
        [SerializeField] private List<LevelConfig> _levels;
        [field: SerializeField] public CurrencyConfig ResetProgressCost { get; private set; }

        public IReadOnlyList<LevelConfig> Levels => _levels;

        public LevelConfig GetBy(int levelNumber)
        {
            int levelIndex = levelNumber - 1;

            return _levels[levelIndex];
        }
    }
}
