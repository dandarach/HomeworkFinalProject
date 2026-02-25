using Assets._Project.Develop.Runtime.Gameplay.InputSystem;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Features.StringServices;
using UnityEngine;
using Assets._Project.Develop.Runtime.Gameplay.Process;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;

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
            
            container.RegisterAsSingle(CreateGameplayInput);
            
            container.RegisterAsSingle<IGameplayCycle>(CreateGameplayCycle);
        }

        private static IGameplayInput CreateGameplayInput(DIContainer c)
            => new GameplayInput();

        private static GameplayCycle CreateGameplayCycle(DIContainer c)
        {
            ICoroutinesPerformer coroutinesPerformer = c.Resolve<ICoroutinesPerformer>();
            GameplayProcess gameplayProcess = c.Resolve<GameplayProcess>();
            SceneSwitcherService sceneSwitcher = c.Resolve<SceneSwitcherService>();

            return new GameplayCycle(gameplayProcess, sceneSwitcher, coroutinesPerformer);
        }

        private static GameplayProcess CreateGameplayProcess(DIContainer c)
        {
            StringGenerator generator = c.Resolve<StringGenerator>();
            StringValidator checker = c.Resolve<StringValidator>();

            return new GameplayProcess(generator, checker);
        }

        private static StringGenerator CreateStringGenerator(DIContainer c)
            => new StringGenerator();

        private static StringValidator CreateStringValidator(DIContainer c)
        {
            IGameplayInput input = c.Resolve<IGameplayInput>();
            
            return new StringValidator(input);
        }
    }
}
