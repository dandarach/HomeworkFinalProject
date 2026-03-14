using System.Collections.Generic;
using Assets._Project.Develop.Runtime.Meta.Features.Stats;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;

namespace Assets._Project.Develop.Runtime.Utilities.DataManagement
{
    public class PlayerData : ISaveData
    {
        public Dictionary<CurrencyTypes, int> WalletData;
        
        public Dictionary<StatTypes, int> StatData;
    }
}
