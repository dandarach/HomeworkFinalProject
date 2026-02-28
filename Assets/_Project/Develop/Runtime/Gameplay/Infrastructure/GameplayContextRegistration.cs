using Assets._Project.Develop.Runtime.Gameplay.InputSystem;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Gameplay.Features.StringServices;
using UnityEngine;
using Assets._Project.Develop.Runtime.Gameplay.Process;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.DataManagement.DataProviders;
using Assets._Project.Develop.Runtime.Gameplay.Statistics;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;

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
                c.Resolve<GameplayProgressService>());

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
