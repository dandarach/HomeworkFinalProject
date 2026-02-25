using Assets._UnityAdvantureProject.Develop.Runtime.Infrastructure.DI;
using UnityEngine;

namespace Assets._UnityAdvantureProject.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayContextRegistration
    {
        public static void Process(DIContainer container, GameplayInputArgs args)
        {
            Debug.Log("Services registration process on the Gamplay scene");
        }
    }
}
