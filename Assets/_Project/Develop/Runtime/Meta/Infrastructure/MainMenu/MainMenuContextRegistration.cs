using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using Assets._Project.Develop.Runtime.Meta.InputSystem;
using Assets._Project.Develop.Runtime.Meta.GameModeChoose;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using UnityEngine;
using Assets._Project.Develop.Runtime.Configs.Meta.Menu;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Gameplay.Statistics;
using Assets._Project.Develop.Runtime.Configs.Gameplay.Levels;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuContextRegistration
    {
        public static void Process(DIContainer container)
        {
            Debug.Log("Services registration process on the Menu scene");

            container.RegisterAsSingle(CreateInputHandler);
            
            container.RegisterAsSingle(CreateStatsResetService);
            
            container.RegisterAsSingle(CreateStatsInfoService);
            
            container.RegisterAsSingle<IGameModeChooseService>(CreateGameModeChooseService);
        }

        private static MainMenuInputHandler CreateInputHandler(DIContainer c)
        {
            MenuConfig config = c.Resolve<ConfigsProviderService>().GetConfig<MenuConfig>();

            return new MainMenuInputHandler(
                config.DigitsGameModeKey,
                config.LettersGameModeKey,
                config.ResetGameProgressKey,
                config.ShowGameProgressKey);
        }

        private static IGameModeChooseService CreateGameModeChooseService(DIContainer c)
            => new GameModeChooseService(
                c.Resolve<MainMenuInputHandler>(),
                c.Resolve<SceneSwitcherService>(),
                c.Resolve<ICoroutinesPerformer>(),
                c.Resolve<StatsInfoService>(),
                c.Resolve<StatsResetService>());

        private static StatsResetService CreateStatsResetService(DIContainer c)
        {
            LevelConfigs config = c.Resolve<ConfigsProviderService>().GetConfig<LevelConfigs>();

            return new StatsResetService(
                c.Resolve<GameplayProgressService>(),
                c.Resolve<WalletService>(),
                config.ResetProgressCost);
        }

        private static StatsInfoService CreateStatsInfoService(DIContainer c)
            => new StatsInfoService(
                c.Resolve<GameplayProgressService>(),
                c.Resolve<WalletService>());
    }
}
