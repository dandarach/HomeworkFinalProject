using System;
using Assets._Project.Develop.Runtime.Configs.Meta.Wallet;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Process
{
    public class GameplayEconomyService : IDisposable
    {
        private readonly GameplayProcess _gameplayProcess;
        private readonly WalletService _wallet;
        
        private CurrencyConfig _winAward;
        private CurrencyConfig _losePenalty;

        public GameplayEconomyService(
            GameplayProcess gameplayProcess,
            WalletService wallet)
        {
            _gameplayProcess = gameplayProcess;
            _wallet = wallet;

            //_gameplayProcess.OnWin += ProcessWin;
            //_gameplayProcess.OnDefeat += ProcessDefeat;
        }

        public void Initialize(CurrencyConfig winAward, CurrencyConfig losePenalty)
        {
            _winAward = winAward;
            _losePenalty = losePenalty;
        }

        public void ProcessWin()
        {
            _wallet.Add(_winAward.Type, _winAward.Value);
        }

        public void ProcessDefeat()
        {
            if (_losePenalty.Value <= 0)
                return;

            if (_wallet.Enough(_winAward.Type, _losePenalty.Value))
                _wallet.Spend(_winAward.Type, _losePenalty.Value);
        }

        public void Dispose()
        {
            //_gameplayProcess.OnWin -= ProcessWin;
            //_gameplayProcess.OnDefeat -= ProcessDefeat;
        }
    }
}
