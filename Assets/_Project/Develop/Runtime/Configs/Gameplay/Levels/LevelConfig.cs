using static Assets._Project.Develop.Runtime.Configs.Meta.Wallet.LevelConfig;
using UnityEngine;
using System.Collections.Generic;
using Assets._Project.Develop.Runtime.Configs.Gameplay.Stages;

namespace Assets._Project.Develop.Runtime.Configs.Gameplay.Levels
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/NewLevelConfig", fileName = "LevelConfig")]
    public partial class LevelConfig : ScriptableObject
    {
        [SerializeField] private List<StageConfig> _stageConfigs;
        [field: SerializeField] public CurrencyConfig WinAward { get; private set; }
        public IReadOnlyList<StageConfig> StageConfigs => _stageConfigs;
    }
}
