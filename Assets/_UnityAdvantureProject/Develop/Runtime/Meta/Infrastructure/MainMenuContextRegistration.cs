using Assets._UnityAdvantureProject.Develop.Runtime.Infrastructure.DI;
using UnityEngine;

namespace Assets._UnityAdvantureProject.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuContextRegistration
    {
        public static void Process(DIContainer container)
        {
            Debug.Log("Services registration process on the Menu scene");
        }
    }
}
