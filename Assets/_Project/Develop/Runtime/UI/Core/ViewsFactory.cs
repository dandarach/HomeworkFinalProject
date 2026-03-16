using System;
using System.Collections.Generic;
using Assets._Project.Develop.Runtime.Utilities.AssetsManagement;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets._Project.Develop.Runtime.UI.Core
{
    public class ViewsFactory
    {
        private readonly ResourcesAssetsLoader _resourcesAssetsLoader;

        private readonly Dictionary<string, string> _viewIDToResourcesPath = new Dictionary<string, string>()
        {
            { ViewIDs.CurrencyView, "UI/Wallet/CurrencyView" },
            { ViewIDs.StatView, "UI/Stats/StatView" },
            { ViewIDs.MainMenuScreen, "UI/MainMenu/MainMenuScreenView" },
            { ViewIDs.LevelTile, "UI/LevelsMenuPopup/LevelTile" },
            { ViewIDs.LevelsMenuPopup, "UI/LevelsMenuPopup/LevelsMenuPopup" },
            { ViewIDs.GameplayScreen, "UI/Gameplay/GameplayScreenView" },
            { ViewIDs.GameplayPopup, "UI/Gameplay/GameplayPopup" },
        };

        public ViewsFactory(ResourcesAssetsLoader resourcesAssetsLoader)
        {
            _resourcesAssetsLoader = resourcesAssetsLoader;
        }

        public TView Create<TView>(string viewID, Transform parent = null) where TView : MonoBehaviour, IView
        {
            if (_viewIDToResourcesPath.TryGetValue(viewID, out string resourcePath) == false)
                throw new ArgumentException($"You didn't set resource path for {typeof(TView)}, searched ID: {viewID}");

            GameObject prefab = _resourcesAssetsLoader.Load<GameObject>(resourcePath);
            GameObject instance = Object.Instantiate(prefab, parent);
            TView view = instance.GetComponent<TView>();

            if (view == null)
                throw new InvalidOperationException($"{typeof(TView)} component not found on the view instance");

            return view;
        }

        public void Release<TView>(TView view) where TView : MonoBehaviour, IView
        {
            Object.Destroy(view.gameObject);
        }
    }
}
