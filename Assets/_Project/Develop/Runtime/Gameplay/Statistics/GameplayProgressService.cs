using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.DataManagement;
using Assets._Project.Develop.Runtime.Utilities.DataManagement.DataProviders;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Statistics
{
    public class GameplayProgressService : IDataReader<PlayerData>, IDataWriter<PlayerData>
    {
        private ReactiveVariable<int> _winCount;
        private ReactiveVariable<int> _loseCount;

        private readonly ICoroutinesPerformer _coroutinesPerformer;
        private readonly PlayerDataProvider _playerDataProvider;

        public GameplayProgressService(ICoroutinesPerformer coroutinesPerformer, PlayerDataProvider playerDataProvider)
        {
            _winCount = new ReactiveVariable<int>(0);
            _loseCount = new ReactiveVariable<int>(0);

            _coroutinesPerformer = coroutinesPerformer;
            _playerDataProvider = playerDataProvider;

            playerDataProvider.RegisterWriter(this);
            playerDataProvider.RegisterReader(this);
        }

        public IReadonlyVariable<int> WinCount => _winCount;
        public IReadonlyVariable<int> LoseCount => _loseCount;

        public void IncreaseWinCount()
        {
            _winCount.Value++;
            SaveGameplayProgress();
        }
        public void IncreaseLoseCount()
        {
            _loseCount.Value++;
            SaveGameplayProgress();
        }

        public void Reset()
        {
            _winCount.Value = 0;
            _loseCount.Value = 0;

            SaveGameplayProgress();

            Debug.Log("- GameplayProgressService reset");
        }

        public void ReadFrom(PlayerData data)
        {
            _winCount.Value = data.WinCount;
            _loseCount.Value = data.LoseCount;

            PrintGameStatistics();
        }

        public void WriteTo(PlayerData data)
        {
            data.WinCount = _winCount.Value;
            data.LoseCount = _loseCount.Value;

            PrintGameStatistics();
        }

        private void SaveGameplayProgress()
        {
            _coroutinesPerformer.StartPerform(_playerDataProvider.Save());
            Debug.Log("PlayerData saved");
        }

        private void LoadGameplayProgress()
        {
            _coroutinesPerformer.StartPerform(_playerDataProvider.Load());
            Debug.Log("PlayerData loaded");
        }

        private void PrintGameStatistics()
        {
            Debug.LogWarning("=== GAME STATISTICS ===");
            Debug.Log($"Win count: {_winCount.Value}");
            Debug.Log($"Lose count: {_loseCount.Value}");
        }
    }
}
