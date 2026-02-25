using System;
using System.Collections;
using System.Collections.Generic;
using Assets._UnityAdvantureProject.Develop.Runtime.Gameplay.Infrastructure;
using Assets._UnityAdvantureProject.Develop.Runtime.Infrastructure;
using Assets._UnityAdvantureProject.Develop.Runtime.Infrastructure.DI;
using Assets._UnityAdvantureProject.Develop.Runtime.Meta.Features.Wallet;
using Assets._UnityAdvantureProject.Develop.Runtime.UI;
using Assets._UnityAdvantureProject.Develop.Runtime.UI.CommonViews;
using Assets._UnityAdvantureProject.Develop.Runtime.UI.Core;
using Assets._UnityAdvantureProject.Develop.Runtime.UI.Wallet;
using Assets._UnityAdvantureProject.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._UnityAdvantureProject.Develop.Runtime.Utilities.DataManagement;
using Assets._UnityAdvantureProject.Develop.Runtime.Utilities.DataManagement.DataProviders;
using Assets._UnityAdvantureProject.Develop.Runtime.Utilities.DataManagement.Serializers;
using Assets._UnityAdvantureProject.Develop.Runtime.Utilities.Reactive;
using Assets._UnityAdvantureProject.Develop.Runtime.Utilities.SceneManagement;
using UnityEngine;

namespace Assets._UnityAdvantureProject.Develop.Runtime.Meta.Infrastructure
{
    public class MainMenuBootstrap : SceneBootstrap
    {
        [SerializeField] private Transform _viewsParent;
        private IconTextView _currencyView;
        private ProjectPresentersFactory _presentersFactory;
        private ViewsFactory _viewsFactory;
        private CurrencyPresenter _currencyPresenter;

        private DIContainer _container;

        private WalletService _walletService;

        private PlayerDataProvider _playerDataProvider;
        private ICoroutinesPerformer _coroutinesPerformer;

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            MainMenuContextRegistration.Process(_container);
        }

        public override IEnumerator Initialize()
        {
            Debug.Log("Main Menu scene initialization");

            _walletService = _container.Resolve<WalletService>();
            _playerDataProvider = _container.Resolve<PlayerDataProvider>();
            _coroutinesPerformer = _container.Resolve<ICoroutinesPerformer>();

            _presentersFactory = _container.Resolve<ProjectPresentersFactory>();
            _viewsFactory = _container.Resolve<ViewsFactory>();

            yield break;
        }

        public override void Run()
        {
            Debug.Log("Main Menu scene start");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                SceneSwitcherService sceneSwitcherService = _container.Resolve<SceneSwitcherService>();
                ICoroutinesPerformer coroutinesPerformer = _container.Resolve<ICoroutinesPerformer>();
                coroutinesPerformer.StartPerform(sceneSwitcherService.ProcessSwitchTo(Scenes.Gameplay, new GameplayInputArgs(2)));
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                _walletService.Add(CurrencyTypes.Gold, 10);
                Debug.Log("Gold: " + _walletService.GetCurrency(CurrencyTypes.Gold).Value);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (_walletService.Enough(CurrencyTypes.Gold, 10))
                {
                    _walletService.Spend(CurrencyTypes.Gold, 10);
                    Debug.Log("Gold: " + _walletService.GetCurrency(CurrencyTypes.Gold).Value);
                }
            }

            if (Input.GetKeyDown(KeyCode.S))
            {
                _coroutinesPerformer.StartPerform(_playerDataProvider.Save());
                Debug.Log("PlayerData saved");
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                _currencyPresenter?.Disable();

                if (_currencyView != null)
                    _viewsFactory.Release(_currencyView);

                _currencyView = _viewsFactory.Create<IconTextView>(ViewIDs.CurrencyView, _viewsParent);

                _currencyPresenter = _presentersFactory.CreateCurrencyPresenter(
                    _currencyView,
                    _walletService.GetCurrency(CurrencyTypes.Gold),
                    CurrencyTypes.Gold);

                _currencyPresenter.Enable();
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                _currencyPresenter?.Disable();

                if (_currencyView != null)
                    _viewsFactory.Release(_currencyView);

                _currencyView = _viewsFactory.Create<IconTextView>(ViewIDs.CurrencyView, _viewsParent);

                _currencyPresenter = _presentersFactory.CreateCurrencyPresenter(
                    _currencyView,
                    _walletService.GetCurrency(CurrencyTypes.Diamond),
                    CurrencyTypes.Diamond);

                _currencyPresenter.Enable();
            }
        }
    }
}
