using System.Collections;
using Assets._UnityAdvantureProject.Develop.Runtime.Infrastructure.DI;
using Assets._UnityAdvantureProject.Develop.Runtime.Utilities.ConfigsManagement;
using Assets._UnityAdvantureProject.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._UnityAdvantureProject.Develop.Runtime.Utilities.DataManagement.DataProviders;
using Assets._UnityAdvantureProject.Develop.Runtime.Utilities.LoadingScreen;
using Assets._UnityAdvantureProject.Develop.Runtime.Utilities.SceneManagement;
using UnityEngine;

namespace Assets._UnityAdvantureProject.Develop.Runtime.Infrastructure.EntryPoint
{
    public class GameEntryPoint : MonoBehaviour
    {
        private void Awake()
        {
            Debug.Log("Start project. Setup settings");

            SetupAppSettings();

            Debug.Log("Servires registration processes");

            DIContainer projectContainer = new DIContainer();

            ProjectContextRegistrations.Process(projectContainer);

            projectContainer.Initialize();

            projectContainer.Resolve<ICoroutinesPerformer>().StartPerform(Initialize(projectContainer));
        }

        private void SetupAppSettings()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 60;
        }

        private IEnumerator Initialize(DIContainer container)
        {
            ILoadingScreen loadingScreen = container.Resolve<ILoadingScreen>();
            SceneSwitcherService sceneSwitcherService = container.Resolve<SceneSwitcherService>();
            PlayerDataProvider playerDataProvider = container.Resolve<PlayerDataProvider>();

            loadingScreen.Show();
            
            Debug.Log("Services initialization is starting");

            yield return container.Resolve<ConfigsProviderService>().LoadAsync();

            bool isPlayerDataSaveExists = false;

            yield return playerDataProvider.Exists(result => isPlayerDataSaveExists = result);

            if (isPlayerDataSaveExists)
                yield return playerDataProvider.Load();
            else
                playerDataProvider.Reset();

                yield return new WaitForSeconds(1f);

            Debug.Log("Services initialization is finishing");

            loadingScreen.Hide();

            yield return sceneSwitcherService.ProcessSwitchTo(Scenes.MainMenu);
        }
    }
}
