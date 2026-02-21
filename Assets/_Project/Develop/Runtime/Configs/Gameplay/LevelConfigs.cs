using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs.Gameplay
{
    [CreateAssetMenu(menuName = "Configs/Gameplay/LevelConfigs", fileName = "LevelConfigs")]
    public class LevelConfigs : ScriptableObject
    {
        [SerializeField] public List<Config> _configs;

        public LevelConfig GetLevelConfig(GameplayMode gameplayMode)
            => _configs.First(config => config.GameplayMode == gameplayMode).LevelConfig;
    }
}
