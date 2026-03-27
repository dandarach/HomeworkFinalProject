using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore;
using Assets._Project.Develop.Runtime.Gameplay.EntitiesCore.Mono;
using Assets._Project.Develop.Runtime.Gameplay.InputSystem;
using Assets._Project.Develop.Runtime.Gameplay.Process;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta.Features.Stats;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.UI.Gameplay;
using Assets._Project.Develop.Runtime.Utilities.AssetsManagement;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayContextRegistration
    {
        public static void Process(DIContainer container)
        {
            container.RegisterAsSingle(CreateGameplayProcess);
            
            container.RegisterAsSingle(CreateGameplayEconomySrevice);

            container.RegisterAsSingle(CreateGameplayPopupService);
            
            container.RegisterAsSingle(CreateGameplayPresentersFactory);

            container.RegisterAsSingle(CreateGameplayGameplayUIRoot);
            
            container.RegisterAsSingle(CreateGameplayInput);
            
            container.RegisterAsSingle<IGameplayCycle>(CreateGameplayCycle);
            
            container.RegisterAsSingle(CreateEntitiesFactory);

            container.RegisterAsSingle(CreateEntitiesLifeContext);
            
            container.RegisterAsSingle(CreateCollidersRegistryService);

            container.RegisterAsSingle(CreateMonoEntitiesFactory).NonLazy();
        }

        private static CollidersRegistryService CreateCollidersRegistryService(DIContainer c)
            => new CollidersRegistryService();

        private static MonoEntitiesFactory CreateMonoEntitiesFactory(DIContainer c)
            => new MonoEntitiesFactory(
                c.Resolve<ResourcesAssetsLoader>(),
                c.Resolve<EntitiesLifeContext>(),
                c.Resolve<CollidersRegistryService>());

        private static EntitiesLifeContext CreateEntitiesLifeContext(DIContainer c)
            => new EntitiesLifeContext();

        private static EntitiesFactory CreateEntitiesFactory(DIContainer c)
            => new EntitiesFactory(c);

        private static GameplayEconomyService CreateGameplayEconomySrevice(DIContainer c)
            => new GameplayEconomyService(
                c.Resolve<GameplayProcess>(),
                c.Resolve<WalletService>());

        private static IGameplayInput CreateGameplayInput(DIContainer c)
            => new GameplayInput();

        private static GameplayPopupService CreateGameplayPopupService(DIContainer c)
            => new GameplayPopupService(
                c.Resolve<ViewsFactory>(),
                c.Resolve<GameplayPresentersFactory>(),
                c.Resolve<GameplayUIRoot>());

        private static GameplayPresentersFactory CreateGameplayPresentersFactory(DIContainer c)
            => new GameplayPresentersFactory(c);

        private static GameplayUIRoot CreateGameplayGameplayUIRoot(DIContainer c)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();

            GameplayUIRoot gameplayUIRootPrefab = resourcesAssetsLoader
                .Load<GameplayUIRoot>("UI/Gameplay/GameplayUIRoot");

            return Object.Instantiate(gameplayUIRootPrefab);
        }

        private static GameplayCycle CreateGameplayCycle(DIContainer c)
            => new GameplayCycle(
                c.Resolve<GameplayProcess>(),
                c.Resolve<GameplayProgressService>(),
                c.Resolve<GameplayPopupService>());

        private static GameplayProcess CreateGameplayProcess(DIContainer c)
            => new GameplayProcess();
    }
}
