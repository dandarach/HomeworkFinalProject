using System;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using UnityEngine;
using static Assets._Project.Develop.Runtime.Configs.Meta.Wallet.LevelConfig;

namespace Assets._Project.Develop.Runtime.Gameplay.Process
{
    public class GameplayEconomyService : IDisposable
    {
        //private readonly GameplayProcess _gameplayProcess;
        private readonly WalletService _wallet;
        
        private CurrencyConfig _winAward;

        public GameplayEconomyService(
            //GameplayProcess gameplayProcess,
            WalletService wallet)
        {
            //_gameplayProcess = gameplayProcess;
            _wallet = wallet;

            //_gameplayProcess.OnWin += ProcessWin;
            //_gameplayProcess.OnDefeat += ProcessDefeat;
        }

        public void Initialize(CurrencyConfig winAward)
        {
            _winAward = winAward;
        }

        public void ProcessWin()
        {
            _wallet.Add(_winAward.Type, _winAward.Value);
        }

        public void Dispose()
        {
            //_gameplayProcess.OnWin -= ProcessWin;
            //_gameplayProcess.OnDefeat -= ProcessDefeat;
        }
    }
}
