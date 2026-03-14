using System;
using System.Collections.Generic;
using Assets._Project.Develop.Runtime.Meta.Features.Stats;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Utilities.AssetsManagement;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.DataManagement;
using Assets._Project.Develop.Runtime.Utilities.DataManagement.DataProviders;
using Assets._Project.Develop.Runtime.Utilities.DataManagement.DataRepository;
using Assets._Project.Develop.Runtime.Utilities.DataManagement.KeyStorage;
using Assets._Project.Develop.Runtime.Utilities.DataManagement.Serializers;
using Assets._Project.Develop.Runtime.Utilities.LoadingScreen;
using Assets._Project.Develop.Runtime.Utilities.Reactive;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using Assets._Project.Develop.Runtime.UI;
using UnityEngine;
using Object = UnityEngine.Object;
using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.Meta.Features.LevelsProgression;

namespace Assets._Project.Develop.Runtime.Infrastructure.EntryPoint
{
    public class ProjectContextRegistrations
    {
        public static void Process(DIContainer container)
        {
            container.RegisterAsSingle<ICoroutinesPerformer>(CreateCoroutinesPerformer);
            
            container.RegisterAsSingle(CreateConfigsProviderService);
            
            container.RegisterAsSingle(CreateResourcesAssetsLoader);
            
            container.RegisterAsSingle(CreateSceneLoaderService);
            
            container.RegisterAsSingle(CreateSceneSwitcherService);
            
            container.RegisterAsSingle<ILoadingScreen>(CreateLoadingScreen);

            container.RegisterAsSingle(CreateWalletService).NonLazy();
            
            container.RegisterAsSingle(CreatePlayerDataProvier);
            
            container.RegisterAsSingle(CreateProjectPresentersFactory);
            
            container.RegisterAsSingle(CreateViewsFactory);

            container.RegisterAsSingle(CreateGameplayProgressService).NonLazy();

            container.RegisterAsSingle(CreateLevelsProgressionService).NonLazy();

            container.RegisterAsSingle<ISaveLoadService>(CreateSaveLoadService);
        }

        private static LevelsProgressionService CreateLevelsProgressionService(DIContainer c)
            => new LevelsProgressionService(c.Resolve<PlayerDataProvider>());

        private static GameplayProgressService CreateGameplayProgressService(DIContainer c)
            => new GameplayProgressService(
                c.Resolve<ICoroutinesPerformer>(),
                c.Resolve<PlayerDataProvider>());
        
        private static ViewsFactory CreateViewsFactory(DIContainer c)
            => new ViewsFactory(c.Resolve<ResourcesAssetsLoader>());

        private static ProjectPresentersFactory CreateProjectPresentersFactory(DIContainer c)
            => new ProjectPresentersFactory(c);

        private static PlayerDataProvider CreatePlayerDataProvier(DIContainer c)
            => new PlayerDataProvider(c.Resolve<ISaveLoadService>(), c.Resolve<ConfigsProviderService>());

        private static SaveLoadService CreateSaveLoadService(DIContainer c)
        {
            IDataSerializer dataSerializer = new JsonSerializer();
            IDataKeysStorage dataKeysStorage = new MapDataKeyStorage();

            string folderPath = Application.isEditor ? Application.dataPath : Application.persistentDataPath;
            IDataRepository dataRepository = new LocalFileDataRepository(folderPath, "json");

            return new SaveLoadService(dataSerializer, dataKeysStorage, dataRepository);
        }

        private static WalletService CreateWalletService(DIContainer c)
        {
            Dictionary<CurrencyTypes, ReactiveVariable<int>> currencies = new();

            foreach (CurrencyTypes currencyType in Enum.GetValues(typeof(CurrencyTypes)))
                currencies[currencyType] = new ReactiveVariable<int>();

            return new WalletService(currencies, c.Resolve<PlayerDataProvider>());
        }

        private static SceneSwitcherService CreateSceneSwitcherService(DIContainer c)
            => new SceneSwitcherService(
                c.Resolve<SceneLoaderService>(),
                c.Resolve<ILoadingScreen>(),
                c);

        private static SceneLoaderService CreateSceneLoaderService(DIContainer c)
            => new SceneLoaderService();

        private static ConfigsProviderService CreateConfigsProviderService(DIContainer c)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();

            ResourcesConfigsLoader resourcesConfigsLoader = new ResourcesConfigsLoader(resourcesAssetsLoader);

            return new ConfigsProviderService(resourcesConfigsLoader);
        }

        private static ResourcesAssetsLoader CreateResourcesAssetsLoader(DIContainer c)
            => new ResourcesAssetsLoader();

        private static CoroutinesPerformer CreateCoroutinesPerformer(DIContainer c)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();

            CoroutinesPerformer coroutinesPerformerPrefab = resourcesAssetsLoader
                .Load<CoroutinesPerformer>("Utilities/CoroutinesPerformer");

            return Object.Instantiate(coroutinesPerformerPrefab);
        }

        private static StandardLoadingScreen CreateLoadingScreen(DIContainer c)
        {
            ResourcesAssetsLoader resourcesAssetsLoader = c.Resolve<ResourcesAssetsLoader>();

            StandardLoadingScreen standardLoadingScreen = resourcesAssetsLoader
                .Load<StandardLoadingScreen>("Utilities/StandardLoadingScreen");

            return Object.Instantiate(standardLoadingScreen);
        }
    }
}
