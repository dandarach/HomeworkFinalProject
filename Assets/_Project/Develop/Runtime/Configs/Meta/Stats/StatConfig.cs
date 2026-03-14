using System;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Configs.Meta.Stats
{
    [Serializable]
    public class StatConfig
    {
        [field: SerializeField] public CurrencyTypes Type { get; private set; }
        [field: SerializeField] public int Value { get; private set; }
    }
}
