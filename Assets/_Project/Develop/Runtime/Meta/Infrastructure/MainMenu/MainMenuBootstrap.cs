using System.Collections;
using Assets._Project.Develop.Runtime.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.Meta.GameModeChoose;
using Assets._Project.Develop.Runtime.UI;
using Assets._Project.Develop.Runtime.UI.CommonViews;
using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.UI.Wallet;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.Meta.Infrastructure.MainMenu
{
    public class MainMenuBootstrap : SceneBootstrap
    {
        [SerializeField] private Transform _viewsParent;
        [SerializeField] private IconTextView _currencyView;

        private IconTextView _winsView;

        private DIContainer _container;
        private IGameModeChooseService _gameModeChooseService;
        private ProjectPresentersFactory _presentersFactory;
        private ViewsFactory _viewsFactory;
        private CurrencyPresenter _currencyPresenter;
        private WalletService _walletService;
        
        private CurrencyPresenter _statsPresenter;

        private bool _isRunning = false;

        public override void ProcessRegistrations(DIContainer container, IInputSceneArgs sceneArgs = null)
        {
            _container = container;

            MainMenuContextRegistration.Process(_container);
        }

        public override IEnumerator Initialize()
        {
            Debug.Log("Main Menu scene initialization");

            ConfigsProviderService configsProviderService = _container.Resolve<ConfigsProviderService>();
            _gameModeChooseService = _container.Resolve<IGameModeChooseService>();

            _walletService = _container.Resolve<WalletService>();

            _presentersFactory = _container.Resolve<ProjectPresentersFactory>();

            _currencyPresenter = _presentersFactory.CreateCurrencyPresenter(
                _currencyView,
                _walletService.GetCurrency(CurrencyTypes.Gold),
                CurrencyTypes.Gold);

            _currencyPresenter.Enable();

            _viewsFactory = _container.Resolve<ViewsFactory>();
            _winsView = _viewsFactory.Create<IconTextView>(ViewIDs.CurrencyView, _viewsParent);

            _statsPresenter = _presentersFactory.CreateCurrencyPresenter(
                _winsView,
                _walletService.GetCurrency(CurrencyTypes.Gold),
                CurrencyTypes.Gold);

            _statsPresenter.Enable();

            yield break;
        }

        public override void Run()
        {
            Debug.Log("Main Menu scene start");
            _isRunning = true;
        }

        private void Update()
        {
            if (_isRunning == false)
                return;

            _gameModeChooseService.Update();
        }
    }
}
