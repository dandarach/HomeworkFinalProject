using UnityEngine;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using System.Collections.Generic;

namespace Assets._Project.Develop.Runtime.Gameplay.Statistics
{
    public class StatsInfoService
    {
        private readonly GameplayProgressService _gameplayProgressService;
        private readonly WalletService _walletService;
        
        public StatsInfoService(
            GameplayProgressService gameplayProgressService,
            WalletService walletService)
        {
            _gameplayProgressService = gameplayProgressService;
            _walletService = walletService;
        }

        public void PrintStats()
        {
            Debug.LogWarning("=== GAME STATISTICS ===");
            PrintGameplayStats();
            PrintWalletStats();
        }

        private void PrintGameplayStats()
        {
            Debug.Log($"Wins: {_gameplayProgressService.WinCount.Value}; Defeats: {_gameplayProgressService.LoseCount.Value}");
        }

        private void PrintWalletStats()
        {
            foreach (KeyValuePair<CurrencyTypes, int> currency in _walletService.GetAllCurrencies())
                Debug.Log($"{currency.Key}: {currency.Value}");
        }
    }
}
