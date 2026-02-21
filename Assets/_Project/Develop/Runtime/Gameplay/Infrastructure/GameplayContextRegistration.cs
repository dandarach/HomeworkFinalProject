using Assets._Project.Develop.Runtime.Gameplay.Infrastructure.InputSystem;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
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
            
            container.RegisterAsSingle(CreateGameplayInput);
        }

        private static IGameplayInput CreateGameplayInput(DIContainer c)
            => new GameplayInput();

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
