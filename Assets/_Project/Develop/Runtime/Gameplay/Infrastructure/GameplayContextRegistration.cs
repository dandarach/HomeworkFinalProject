using Assets._Project.Develop.Runtime.Infrastructure.DI;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayContextRegistration
    {
        public static void Process(DIContainer container, GameplayInputArgs args)
        {
            Debug.Log("Services registration process on the Gamplay scene");

            container.RegisterAsSingle(CreateGameplayCycle);
            container.RegisterAsSingle(CreateGameplayProcess);
        }

        private static GameplayCycle CreateGameplayCycle(DIContainer c)
            => new GameplayCycle();
        
        private static GameplayProcess CreateGameplayProcess(DIContainer c)
            => new GameplayProcess();
    }
}
