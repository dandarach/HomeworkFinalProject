using Assets._Project.Develop.Runtime.Gameplay.Features.StringServices;
using Assets._Project.Develop.Runtime.Gameplay.InputSystem;
using Assets._Project.Develop.Runtime.Gameplay.Process;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta.Features.Stats;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.UI;
using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.UI.Gameplay;
using Assets._Project.Develop.Runtime.UI.MainMenu;
using Assets._Project.Develop.Runtime.Utilities.AssetsManagement;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.DataManagement.DataProviders;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayContextRegistration
    {
        public static void Process(DIContainer container)
        {
            Debug.Log("Services registration process on the Gamplay scene");

            container.RegisterAsSingle(CreateStringGenerator);

            container.RegisterAsSingle(CreateStringValidator);
            
            container.RegisterAsSingle(CreateGameplayProcess);
            
            container.RegisterAsSingle(CreateGameplayEconomySrevice);
            
            container.RegisterAsSingle(CreateGameplayInput);
            
            container.RegisterAsSingle<IGameplayCycle>(CreateGameplayCycle);

            container.RegisterAsSingle(CreateGameplayUIRoot).NonLazy();
            
            container.RegisterAsSingle(CreateGameplayPresentersFactory);

            container.RegisterAsSingle(CreateGameplayScreenPresenter).NonLazy();

            container.RegisterAsSingle(CreateGameplayPopupService);
        }

        private static GameplayPopupService CreateGameplayPopupService(DIContainer c)
        {
            return new GameplayPopupService(
                c.Resolve<ViewsFactory>(),
                c.Resolve<GameplayPresentersFactory>(),
                c.Resolve<GameplayUIRoot>());
        }

        private static GameplayUIRoot CreateGameplayUIRoot(DIContainer c)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();

            GameplayUIRoot gameplayUIRootPrefab = resourcesAssetsLoader
                .Load<GameplayUIRoot>("UI/Gameplay/GameplayUIRoot");

            return Object.Instantiate(gameplayUIRootPrefab);
        }

        private static GameplayPresentersFactory CreateGameplayPresentersFactory(DIContainer c)
            => new GameplayPresentersFactory(c);

        private static GameplayScreenPresenter CreateGameplayScreenPresenter(DIContainer c)
        {
            GameplayUIRoot uiRoot = c.Resolve<GameplayUIRoot>();

            GameplayScreenView view = c
                .Resolve<ViewsFactory>()
                .Create<GameplayScreenView>(ViewIDs.GameplayScreen, uiRoot.HUDLayer);

            GameplayScreenPresenter presenter = c
                .Resolve<GameplayPresentersFactory>()
                .CreateGameplayScreen(view);

            return presenter;
        }

        private static GameplayEconomyService CreateGameplayEconomySrevice(DIContainer c)
            => new GameplayEconomyService(
                c.Resolve<GameplayProcess>(),
                c.Resolve<WalletService>());

        private static IGameplayInput CreateGameplayInput(DIContainer c)
            => new GameplayInput();

        private static GameplayCycle CreateGameplayCycle(DIContainer c)
            => new GameplayCycle(
                c.Resolve<GameplayProcess>(),
                c.Resolve<SceneSwitcherService>(),
                c.Resolve<ICoroutinesPerformer>(),
                c.Resolve<GameplayProgressService>(),
                c.Resolve<GameplayPopupService>());

        private static GameplayProcess CreateGameplayProcess(DIContainer c)
            => new GameplayProcess(
                c.Resolve<StringGenerator>(),
                c.Resolve<StringValidator>());

        private static StringGenerator CreateStringGenerator(DIContainer c)
            => new StringGenerator();

        private static StringValidator CreateStringValidator(DIContainer c)
            => new StringValidator(c.Resolve<IGameplayInput>());
    }
}
