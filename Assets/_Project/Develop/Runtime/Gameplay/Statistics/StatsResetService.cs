using UnityEngine;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;

namespace Assets._Project.Develop.Runtime.Gameplay.Statistics
{
    public class StatsResetService
    {
        private readonly GameplayProgressService _gameplayProgressService;
        private readonly WalletService _walletService;
        
        private readonly CurrencyTypes _resetCurrencyType;
        private readonly int _resetCost;

        public StatsResetService(
            GameplayProgressService gameplayProgressService,
            WalletService walletService,
            CurrencyTypes resetCurrencyType,
            int resetCost)
        {
            _gameplayProgressService = gameplayProgressService;
            _walletService = walletService;
            _resetCurrencyType = resetCurrencyType;
            _resetCost = resetCost;
        }

        public void Reset()
        {
            if (_walletService.Enough(CurrencyTypes.Gold, _resetCost) == false)
            {
                Debug.LogWarning($"Not enough {_resetCost} {_resetCurrencyType} to reset gameplay statistics");
                return;
            }
            else
            {
                _gameplayProgressService.Reset();
                _walletService.Spend(_resetCurrencyType, _resetCost);
                Debug.Log($"Reset Game Statistics. Cost: {_resetCost} {_resetCurrencyType}");
            }
        }
    }
}
