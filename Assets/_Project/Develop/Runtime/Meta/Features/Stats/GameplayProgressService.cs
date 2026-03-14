using System;
using System.Collections.Generic;
using System.Linq;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.DataManagement;
using Assets._Project.Develop.Runtime.Utilities.DataManagement.DataProviders;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Features.Stats
{
    public class GameplayProgressService : IDataReader<PlayerData>, IDataWriter<PlayerData>
    {
        private readonly Dictionary<StatTypes, ReactiveVariable<int>> _stats;
        private readonly ICoroutinesPerformer _coroutinesPerformer;
        private readonly PlayerDataProvider _playerDataProvider;

        public GameplayProgressService(
            Dictionary<StatTypes, ReactiveVariable<int>> stats,
            ICoroutinesPerformer coroutinesPerformer,
            PlayerDataProvider playerDataProvider)
        {
            _coroutinesPerformer = coroutinesPerformer;
            _playerDataProvider = playerDataProvider;

            _stats = new Dictionary<StatTypes, ReactiveVariable<int>>(stats);

            playerDataProvider.RegisterWriter(this);
            playerDataProvider.RegisterReader(this);
        }

        public List<StatTypes> AvailableStats => _stats.Keys.ToList();

        public IReadonlyVariable<int> GetStat(StatTypes type) => _stats[type];

        public IReadOnlyDictionary<StatTypes, int> GetAllStats()
        {
            return _stats.ToDictionary(
                pair => pair.Key,
                pair => pair.Value.Value);
        }

        public void Reset()
        {
            foreach (ReactiveVariable<int> stat in _stats.Values)
                stat.Value = 0;

            SaveGameplayProgress();
        }

        public void ReadFrom(PlayerData data)
        {
            foreach (KeyValuePair<StatTypes, int> stat in data.StatData)
            {
                if (_stats.ContainsKey(stat.Key))
                    _stats[stat.Key].Value = stat.Value;
                else
                    _stats.Add(stat.Key, new ReactiveVariable<int>(stat.Value));
            }
        }

        public void WriteTo(PlayerData data)
        {
            foreach (KeyValuePair<StatTypes, ReactiveVariable<int>> stat in _stats)
            {
                if (data.StatData.ContainsKey(stat.Key))
                    data.StatData[stat.Key] = stat.Value.Value;
                else
                    data.StatData.Add(stat.Key, stat.Value.Value);
            }
        }

        public void SaveGameplayProgress()
        {
            _coroutinesPerformer.StartPerform(_playerDataProvider.Save());
            Debug.Log("PlayerData saved");
        }

        public void ProcessWin()
        {
            // TODO: NEED TO FIX
            //_winCount.Value++;
            SaveGameplayProgress();
        }

        public void ProcessDefeat()
        {
            // TODO: NEED TO FIX
            //_loseCount.Value++;
            SaveGameplayProgress();
        }
    }
}
