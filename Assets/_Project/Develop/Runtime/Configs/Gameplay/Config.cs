using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs.Gameplay
{
    [Serializable]
    public class Config
    {
        [field: SerializeField] public GameplayMode GameplayMode {  get; private set; }
        [field: SerializeField] public LevelConfig LevelConfig { get; private set; }
    }
}
