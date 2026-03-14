using System;
using System.Collections.Generic;
using System.Linq;
using Assets._Project.Develop.Runtime.Meta.Features.Stats;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs.Meta.Stats
{
    [CreateAssetMenu(menuName = "Configs/Meta/Stats/NewStatIconConfig", fileName = "StatIconConfig")]
    public class StatIconsConfig : ScriptableObject
    {
        [SerializeField] private List<StatConfig> _configs;

        public Sprite GetSpriteFor(StatTypes statType)
            => _configs.First(config => config.Type == statType).Sprite;

        [Serializable]
        private class StatConfig
        {
            [field: SerializeField] public StatTypes Type { get; private set; }
            [field: SerializeField] public Sprite Sprite { get; private set; }
        }
    }
}
