using UnityEngine;
using Assets._Project.Develop.Runtime.Configs.Gameplay;
using Assets._Project.Develop.Runtime.Gameplay.Infrastructure;
using Assets._Project.Develop.Runtime.Gameplay.Statistics;
using Assets._Project.Develop.Runtime.Meta.InputSystem;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;

namespace Assets._Project.Develop.Runtime.Meta.GameModeChoose
{
    public class GameModeChooseService : IGameModeChooseService
    {
        private readonly IMainMenuInput _input;
        private readonly SceneSwitcherService _sceneSwitcher;
        private readonly ICoroutinesPerformer _coroutinesPerformer;
        private readonly StatsInfoService _statsInfoService;
        private readonly StatsResetService _statsResetService;

        public GameModeChooseService(
            IMainMenuInput input,
            SceneSwitcherService sceneSwitcher,
            ICoroutinesPerformer coroutinesPerformer,
            StatsInfoService statsInfoService,
            StatsResetService statsResetService)
            {
                _input = input;
                _sceneSwitcher = sceneSwitcher;
                _coroutinesPerformer = coroutinesPerformer;
                _statsInfoService = statsInfoService;
                _statsResetService = statsResetService;
            }

        public void Update()
        {
            if (_input.DigitsGameModeSelected)
            {
                SwitchToGameplay(GameplayMode.Digits);
                return;
            }

            if (_input.LettersGameModeSelected)
            {
                SwitchToGameplay(GameplayMode.Letters);
                return;
            }

            if (_input.ResetGameProgressKeyPressed)
            {
                ResetGameProgress();
                return;
            }

            if (_input.ShowGameProgressKeyPressed)
            {
                ShowGameProgress();
                return;
            }
        }

        private void ShowGameProgress() => _statsInfoService.PrintStats();

        private void ResetGameProgress() => _statsResetService.Reset();

        private void SwitchToGameplay(GameplayMode mode)
            => _coroutinesPerformer.StartPerform(_sceneSwitcher.ProcessSwitchTo(Scenes.Gameplay, new GameplayInputArgs(mode)));
    }
}
