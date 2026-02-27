using Assets._Project.Develop.Runtime.Configs.Gameplay.Levels;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.DataManagement;
using Assets._Project.Develop.Runtime.Utilities.DataManagement.DataProviders;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Process
{
    public class GameplayCycle : IGameplayCycle,  IDataReader<PlayerData>, IDataWriter<PlayerData>
    {
        private const string RestartGameMessage = "TO RESTART THE GAME";
        private const string GoToMainMenuMessage = "FOR MAIN MENU";

        private readonly GameplayProcess _gameplayProcess;
        private readonly SceneSwitcherService _sceneSwitcher;
        private readonly ICoroutinesPerformer _coroutinesPerformer;

        private LevelConfig _levelConfig;
        private GameState _gameState;

        private int _winCount = 0;
        private int _loseCount = 0;

        public GameplayCycle(
            GameplayProcess gameplayProcess,
            SceneSwitcherService sceneSwitcher,
            ICoroutinesPerformer coroutinesPerformer,
            PlayerDataProvider playerDataProvider)
        {
            _gameplayProcess = gameplayProcess;
            _sceneSwitcher = sceneSwitcher;
            _coroutinesPerformer = coroutinesPerformer;

            playerDataProvider.RegisterWriter(this);
            playerDataProvider.RegisterReader(this);
        }

        public void Run(LevelConfig config)
        {
            _levelConfig = config;

            _gameplayProcess.OnWin += OnWin;
            _gameplayProcess.OnDefeat += OnDefeat;

            _gameState = GameState.Running;
            _gameplayProcess.Run(_levelConfig.Symbols, _levelConfig.SymbolsToGuess);

            PrintGameStatistics();
        }

        public void Update()
        {
            _gameplayProcess.Update();

            if (_gameState == GameState.Running)
                return;

            if (Input.GetKeyDown(_levelConfig.RestartGameKey))
            {
                if (_gameState == GameState.Win)
                    SwitchToMainMenu();
                else if (_gameState == GameState.Defeat)
                    Restart();
            }
        }

        private void Restart()
        {
            OnGameEnded();
            Run(_levelConfig);
        }

        private void SwitchToMainMenu()
        {
            _coroutinesPerformer.StartPerform(_sceneSwitcher.ProcessSwitchTo(Scenes.MainMenu));
        }

        private void OnWin()
        {
            Debug.LogWarning("*** WIN ***");
            Debug.LogWarning($"PRESS {_levelConfig.RestartGameKey} {GoToMainMenuMessage}");
            
            _gameState = GameState.Win;
            _winCount++;
            
            OnGameEnded();
        }

        private void OnDefeat()
        {
            Debug.LogWarning("*** DEFEAT ***");
            Debug.LogWarning($"PRESS {_levelConfig.RestartGameKey} {RestartGameMessage}");
            
            _gameState = GameState.Defeat;
            _loseCount++;

            OnGameEnded();
        }

        private void OnGameEnded()
        {
            _gameplayProcess.OnWin -= OnWin;
            _gameplayProcess.OnDefeat -= OnDefeat;
            _gameplayProcess.Dispose();

            PrintGameStatistics();
        }

        public void ReadFrom(PlayerData data)
        {
            _winCount = data.WinCount;
            _loseCount = data.LoseCount;
        }

        public void WriteTo(PlayerData data)
        {
            data.WinCount = _winCount;
            data.LoseCount = _loseCount;
        }

        private void PrintGameStatistics()
        {
            Debug.LogWarning("=== GAME STATISTICS ===");
            Debug.Log($"Win count: {_winCount}");
            Debug.Log($"Lose count: {_loseCount}");
        }
    }
}
