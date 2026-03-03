using Assets._Project.Develop.Runtime.Configs.Gameplay.Levels;
using Assets._Project.Develop.Runtime.Configs.Meta.Menu;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta.Features.Statistics;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Meta.GameModeChoose;
using Assets._Project.Develop.Runtime.Meta.InputSystem;
using Assets._Project.Develop.Runtime.UI;
using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.UI.MainMenu;
using Assets._Project.Develop.Runtime.Utilities.AssetsManagement;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using UnityEngine;

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

            container.RegisterAsSingle(CreateMainMenuUIRoot).NonLazy();

            container.RegisterAsSingle(CreateMainMenuPresentersFactory);

            container.RegisterAsSingle(CreateMainMenuScreenPresenter).NonLazy();
            
            container.RegisterAsSingle(CreateMainMenuPopupService);

            container.RegisterAsSingle<IGameModeChooseService>(CreateGameModeChooseService);
        }

        private static MainMenuPopupService CreateMainMenuPopupService(DIContainer c)
        {
            return new MainMenuPopupService(
                c.Resolve<ViewsFactory>(),
                c.Resolve<ProjectPresentersFactory>(),
                c.Resolve<MainMenuUIRoot>());
        }

        private static MainMenuUIRoot CreateMainMenuUIRoot(DIContainer c)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();

            MainMenuUIRoot mainMenuUIRootPrefab = resourcesAssetsLoader
                .Load<MainMenuUIRoot>("UI/MainMenu/MainMenuUIRoot");

            return Object.Instantiate(mainMenuUIRootPrefab);
        }

        private static MainMenuPresentersFactory CreateMainMenuPresentersFactory(DIContainer c)
            => new MainMenuPresentersFactory(c);

        private static MainMenuScreenPresenter CreateMainMenuScreenPresenter(DIContainer c)
        {
            MainMenuUIRoot uiRoot = c.Resolve<MainMenuUIRoot>();
            
            MainMenuScreenView view = c
                .Resolve<ViewsFactory>()
                .Create<MainMenuScreenView>(ViewIDs.MainMenuScreen, uiRoot.HUDLayer);

            MainMenuScreenPresenter presenter = c
                .Resolve<MainMenuPresentersFactory>()
                .CreateMainMenuScreen(view);

            return presenter;
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
