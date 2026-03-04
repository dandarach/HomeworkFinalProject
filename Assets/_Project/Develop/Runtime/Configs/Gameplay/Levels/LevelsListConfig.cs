using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Assets._Project.Develop.Runtime.Configs.Meta.Wallet.LevelConfig;

namespace Assets._Project.Develop.Runtime.Configs.Gameplay.Levels
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/LevelConfigs", fileName = "LevelConfigs")]
    public class LevelsListConfig : ScriptableObject
    {
        [SerializeField] public List<LevelConfig> _levels;
        
        [field: SerializeField] public CurrencyConfig ResetProgressCost { get; private set; }

        public IReadOnlyList<LevelConfig> Levels => _levels;

        public LevelConfig GetLevelConfig(GameplayMode gameplayMode)
            // TODO: NEED TO FIX
            //=> _levels.First(config => config.GameplayMode == gameplayMode).LevelConfig;
            => GetBy(1);

        public LevelConfig GetBy(int levelNumber)
        {
            int levelIndex = levelNumber - 1;

            return _levels[levelIndex];
        }
    }
}
