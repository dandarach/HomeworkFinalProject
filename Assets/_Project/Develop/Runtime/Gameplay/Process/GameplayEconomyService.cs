using System;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Process
{
    public class GameplayEconomyService : IDisposable
    {
        private readonly GameplayProcess _gameplayProcess;
        private readonly WalletService _wallet;
        //private readonly GameplayEconomyConfig _config;

        public GameplayEconomyService(
            GameplayProcess gameplayProcess,
            WalletService wallet)
            //GameplayEconomyConfig config)
        {
            _gameplayProcess = gameplayProcess;
            _wallet = wallet;
            //_config = config;

            _gameplayProcess.OnWin += ProcessWin;
            _gameplayProcess.OnDefeat += ProcessDefeat;
            
            Debug.LogWarning(_gameplayProcess);
        }

        public void ProcessWin()
        {
            Debug.LogWarning("GameplayEconomyService.ProcessWin()");
            _wallet.Add(CurrencyTypes.Gold, 10);/////////////////////////////////////
        }

        public void ProcessDefeat()
        {
            //if (_config.LossPenalty <= 0)
            //    return;

            Debug.LogWarning("GameplayEconomyService.ProcessDefeat()");

            if (_wallet.Enough(CurrencyTypes.Gold, 10))////////////////////////////
                _wallet.Spend(CurrencyTypes.Gold, 10);/////////////////////////////
        }

        public void Dispose()
        {
            _gameplayProcess.OnWin -= ProcessWin;
            _gameplayProcess.OnDefeat -= ProcessDefeat;
        }
    }
}
