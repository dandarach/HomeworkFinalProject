using System.Collections.Generic;
using System.Linq;
using Assets._Project.Develop.Runtime.Configs.Meta.Wallet;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs.Gameplay.Levels
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/LevelConfigs", fileName = "LevelConfigs")]
    public class LevelsListConfig : ScriptableObject
    {
        [SerializeField] public List<LevelConfig> _levels;
        
        [field: SerializeField] public CurrencyConfig ResetProgressCost { get; private set; }

        public IReadOnlyList<LevelConfig> Levels => _levels;

        public LevelConfig GetLevelConfig(GameplayMode mode)
            //=> _levels.First(config => config.GameplayMode == mode).LevelConfig;
            => _levels[1];

    }
}
