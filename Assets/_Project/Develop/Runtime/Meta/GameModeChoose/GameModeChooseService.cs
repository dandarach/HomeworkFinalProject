using Assets._Project.Develop.Runtime.Gameplay.Infrastructure;
using Assets._Project.Develop.Runtime.Meta.Configs;
using Assets._Project.Develop.Runtime.Meta.InputSystem;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;

namespace Assets._Project.Develop.Runtime.Meta.GameModeChoose
{
    public class GameModeChooseService : IGameModeChooseService
    {
        private IMainMenuInput _input;
        private LevelConfigs _levelConfigs;
        private SceneSwitcherService _sceneSwitcher;
        private ICoroutinesPerformer _coroutinesPerformer;

        public GameModeChooseService(
            IMainMenuInput input,
            LevelConfigs levelConfigs,
            SceneSwitcherService sceneSwitcher,
            ICoroutinesPerformer coroutinesPerformer)
            {
                _input = input;
                _levelConfigs = levelConfigs;
                _sceneSwitcher = sceneSwitcher;
                _coroutinesPerformer = coroutinesPerformer;
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
            }
        }

        private void SwitchToGameplay(GameplayMode mode)
        {
            LevelConfig levelConfig = _levelConfigs.GetLevelConfig(mode);
            GameplayInputArgs args = new GameplayInputArgs(levelConfig);

            _coroutinesPerformer.StartPerform(_sceneSwitcher.ProcessSwitchTo(Scenes.Gameplay, args));
        }
    }
}
