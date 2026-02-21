using Assets._Project.Develop.Runtime.Configs;
using Assets._Project.Develop.Runtime.Configs.Gameplay;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayContextRegistration
    {
        public static void Process(DIContainer container)
        {
            Debug.Log("Services registration process on the Gamplay scene");

            container.RegisterAsSingle(CreateStringGenerator);

            container.RegisterAsSingle(CreateStringChecker);
            
            container.RegisterAsSingle(CreateGameplayProcess);
        }

        private static GameplayProcess CreateGameplayProcess(DIContainer c)
        {
            StringGenerator generator = c.Resolve<StringGenerator>();
            StringValidator checker = c.Resolve<StringValidator>();

            return new GameplayProcess(generator, checker);
        }

        private static StringGenerator CreateStringGenerator(DIContainer c)
            => new StringGenerator();

        private static StringValidator CreateStringChecker(DIContainer c)
            => new StringValidator();
    }
}
