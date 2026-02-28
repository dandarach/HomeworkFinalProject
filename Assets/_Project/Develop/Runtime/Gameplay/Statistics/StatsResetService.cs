using UnityEngine;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;

namespace Assets._Project.Develop.Runtime.Gameplay.Statistics
{
    public class StatsResetService
    {
        private readonly GameplayProgressService _gameplayProgressService;
        private readonly WalletService _wallet;
        
        private readonly CurrencyTypes _resetCurrencyType;
        private readonly int _resetCost;

        public StatsResetService(
            GameplayProgressService gameplayProgressService,
            WalletService wallet,
            CurrencyTypes resetCurrencyType,
            int resetCost)
        {
            _gameplayProgressService = gameplayProgressService;
            _wallet = wallet;
            _resetCurrencyType = resetCurrencyType;
            _resetCost = resetCost;
        }

        public void Reset()
        {
            if (_wallet.Enough(CurrencyTypes.Gold, _resetCost) == false)
            {
                Debug.LogWarning($"Not enough {_resetCost} {_resetCurrencyType} to reset gameplay statistics");
                return;
            }
            else
            {
                _wallet.Spend(_resetCurrencyType, _resetCost);
                _gameplayProgressService.Reset();
                Debug.Log($"Reset Game Statistics. Cost: {_resetCost} {_resetCurrencyType}");
            }
        }
    }
}
