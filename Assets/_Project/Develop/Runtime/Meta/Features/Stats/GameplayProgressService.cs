using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.DataManagement;
using Assets._Project.Develop.Runtime.Utilities.DataManagement.DataProviders;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Features.Stats
{
    public class GameplayProgressService : IDataReader<PlayerData>, IDataWriter<PlayerData>
    {
        private readonly ICoroutinesPerformer _coroutinesPerformer;
        private readonly PlayerDataProvider _playerDataProvider;

        private ReactiveVariable<int> _winCount;
        private ReactiveVariable<int> _loseCount;

        public GameplayProgressService(
            ICoroutinesPerformer coroutinesPerformer,
            PlayerDataProvider playerDataProvider)
        {
            _coroutinesPerformer = coroutinesPerformer;
            _playerDataProvider = playerDataProvider;

            _winCount = new ReactiveVariable<int>(0);
            _loseCount = new ReactiveVariable<int>(0);

            playerDataProvider.RegisterWriter(this);
            playerDataProvider.RegisterReader(this);
        }

        public IReadonlyVariable<int> WinCount => _winCount;
        public IReadonlyVariable<int> LoseCount => _loseCount;

        public void Reset()
        {
            _winCount.Value = 0;
            _loseCount.Value = 0;

            SaveGameplayProgress();
        }

        public void ReadFrom(PlayerData data)
        {
            _winCount.Value = data.WinCount;
            _loseCount.Value = data.LoseCount;
        }

        public void WriteTo(PlayerData data)
        {
            data.WinCount = _winCount.Value;
            data.LoseCount = _loseCount.Value;
        }

        public void SaveGameplayProgress()
        {
            _coroutinesPerformer.StartPerform(_playerDataProvider.Save());
            Debug.Log("PlayerData saved");
        }

        public void ProcessWin()
        {
            _winCount.Value++;
            SaveGameplayProgress();
        }

        public void ProcessDefeat()
        {
            _loseCount.Value++;
            SaveGameplayProgress();
        }
    }
}
