using System.Collections.Generic;
using Assets._UnityAdvantureProject.Develop.Runtime.Meta.Features.Wallet;

namespace Assets._UnityAdvantureProject.Develop.Runtime.Utilities.DataManagement
{
    public class PlayerData : ISaveData
    {
        public Dictionary<CurrencyTypes, int> WalletData;
    }
}
