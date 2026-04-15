using System;
using Assets._Project.Develop.Runtime.Configs.Gameplay.Stages;
using Assets._Project.Develop.Runtime.Gameplay.Features.Enemies;
using Assets._Project.Develop.Runtime.Utilities.Reactive;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.StagesFeature
{
    public class ClearAllEnemiesStage : IStage
    {
        private ClearAllEnemiesStageConfig _config;
        private ReactiveEvent _completed = new();
        private EnemiesFactory _enemiesFactory;
        private bool _inProcess;

        public ClearAllEnemiesStage(
            ClearAllEnemiesStageConfig config,
            EnemiesFactory enemiesFactory)
        {
            _config = config;
            _enemiesFactory = enemiesFactory;
        }

        public IReadonlyEvent Completed => _completed;

        public void Cleanup()
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public void Start()
        {
            if (_inProcess)
                throw new InvalidOperationException("Game mode already started");

            SpawnEnemies();

            _inProcess = true;
        }

        public void Update(float deltaTime)
        {
            throw new System.NotImplementedException();
        }

        private void SpawnEnemies()
        {
            foreach (EnemyItemConfig enemyItemConfig in _config.EnemyItems)
                SpawnEnemy(enemyItemConfig);
        }

        private void SpawnEnemy(EnemyItemConfig enemyItemConfig)
        {
            _enemiesFactory.Create(enemyItemConfig.SpawnPosition, enemyItemConfig.EnemyConfig);
        }
    }
}
