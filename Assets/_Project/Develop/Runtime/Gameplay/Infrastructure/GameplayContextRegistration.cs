using Assets._Project.Develop.Runtime.Configs;
using Assets._Project.Develop.Runtime.Configs.Gameplay;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayContextRegistration
    {
        public static void Process(DIContainer container, GameplayInputArgs args)
        {
            Debug.Log("Services registration process on the Gamplay scene");

            container.RegisterAsSingle(CreateStringGenerator);

            container.RegisterAsSingle(CreateStringChecker);
            
            container.RegisterAsSingle(CreateGameManager);
            
            container.RegisterAsSingle(CreateGameplayProcess);
        }

        private static GameplayProcess CreateGameplayProcess(DIContainer c)
        {
            StringGenerator generator = c.Resolve<StringGenerator>();
            StringChecker checker = c.Resolve<StringChecker>();
            GameManager manager = c.Resolve<GameManager>();

            return new GameplayProcess(generator, checker, manager);
        }

        private static StringGenerator CreateStringGenerator(DIContainer c)
        {
            LevelConfig config = c.Resolve<ConfigsProviderService>().GetConfig<LevelConfig>();
            
            return new StringGenerator(config.Symbols, config.SymbolsToGuess);
        }

        private static StringChecker CreateStringChecker(DIContainer c)
            => new StringChecker();

        private static GameManager CreateGameManager(DIContainer c)
            => new GameManager();
    }
}
