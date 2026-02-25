using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using Assets._Project.Develop.Runtime.Meta.InputSystem;
using Assets._Project.Develop.Runtime.Meta.Configs;
using Assets._Project.Develop.Runtime.Meta.GameModeChoose;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuContextRegistration
    {
        public static void Process(DIContainer container)
        {
            Debug.Log("Services registration process on the Menu scene");

            container.RegisterAsSingle(CreateInputHandler);
            
            container.RegisterAsSingle<IGameModeChooseService>(CreateGameModeChooseService);
        }

        private static MainMenuInputHandler CreateInputHandler(DIContainer c)
        {
            MenuConfig config = c.Resolve<ConfigsProviderService>().GetConfig<MenuConfig>();

            return new MainMenuInputHandler(config.DigitsGameModeKey, config.LettersGameModeKey);
        }

        private static IGameModeChooseService CreateGameModeChooseService(DIContainer c)
        {
            IMainMenuInput input = c.Resolve<MainMenuInputHandler>();

            SceneSwitcherService sceneSwitcher = c.Resolve<SceneSwitcherService>();

            ICoroutinesPerformer coroutinesPerformer = c.Resolve<ICoroutinesPerformer>();

            return new GameModeChooseService(input, sceneSwitcher, coroutinesPerformer);
        }
    }
}
