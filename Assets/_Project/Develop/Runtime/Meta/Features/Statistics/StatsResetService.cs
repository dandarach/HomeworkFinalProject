using UnityEngine;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using static Assets._Project.Develop.Runtime.Configs.Meta.Wallet.LevelConfig;

namespace Assets._Project.Develop.Runtime.Meta.Features.Statistics
{
    public class StatsResetService
    {
        private readonly GameplayProgressService _gameplayProgressService;
        private readonly WalletService _wallet;
        
        private readonly CurrencyConfig _resetCost;

        public StatsResetService(
            GameplayProgressService gameplayProgressService,
            WalletService wallet,
            CurrencyConfig resetCost)
        {
            _gameplayProgressService = gameplayProgressService;
            _wallet = wallet;
            _resetCost = resetCost;
        }

        public void Reset()
        {
            if (_wallet.Enough(_resetCost.Type, _resetCost.Value) == false)
            {
                Debug.LogWarning($"Not enough {_resetCost.Type} {_resetCost.Value} to reset gameplay statistics");
                return;
            }
            else
            {
                _wallet.Spend(_resetCost.Type, _resetCost.Value);
                _gameplayProgressService.Reset();
                Debug.Log($"Reset Game Statistics. Cost: {_resetCost.Type} {_resetCost.Value}");
            }
        }
    }
}
