using Assets._Project.Develop.Runtime.Configs;
using Assets._Project.Develop.Runtime.Gameplay.Infrastructure;
using Assets._Project.Develop.Runtime.Gameplay.InputSystem;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Utilities.AssetsManagement;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuContextRegistration
    {
        public static void Process(DIContainer container)
        {
            Debug.Log("Services registration process on the Menu scene");

            container.RegisterAsSingle(CreateInputHandler);
        }

        private static MainMenuInputHandler CreateInputHandler(DIContainer c)
        {
            MenuConfig config = c.Resolve<ConfigsProviderService>().GetConfig<MenuConfig>();

            return new MainMenuInputHandler(config.SelectDigitsGameModeKey, config.SelectLettersGameModeKey);
        }
    }
}
