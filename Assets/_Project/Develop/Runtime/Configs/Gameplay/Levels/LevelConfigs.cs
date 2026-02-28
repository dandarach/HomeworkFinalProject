using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Assets._Project.Develop.Runtime.Configs.Meta.Wallet.LevelConfig;

namespace Assets._Project.Develop.Runtime.Configs.Gameplay.Levels
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/LevelConfigs", fileName = "LevelConfigs")]
    public class LevelConfigs : ScriptableObject
    {
        [SerializeField] public List<Config> _configs;
        [field: SerializeField] public CurrencyConfig ResetProgressCost { get; private set; }

        public LevelConfig GetLevelConfig(GameplayMode gameplayMode)
            => _configs.First(config => config.GameplayMode == gameplayMode).LevelConfig;
    }
}
