using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.UI.Gameplay.Popups
{
    public class WinPopupPresenter : PopupPresenterBase
    {
        private const string TitleName = "GAME OVER";
        private const string WinMessage = "YOU WIN!";

        private readonly WinPopupView _view;
        private readonly SceneSwitcherService _sceneSwitcher;
        private readonly ICoroutinesPerformer _coroutinesPerformer;

        public WinPopupPresenter(
            ICoroutinesPerformer coroutinesPerformer,
            WinPopupView view,
            SceneSwitcherService switcherService) : base(coroutinesPerformer)
        {
            _coroutinesPerformer = coroutinesPerformer;
            _view = view;
            _sceneSwitcher = switcherService;
        }

        protected override PopupViewBase PopupView => _view;

        public override void Initialize()
        {
            base.Initialize();

            _view.SetTitle(TitleName);
            _view.SetMessageText(WinMessage);

            _view.ButtonClicked += OnButtonClicked;
        }

        public void SetTitle(string title) => _view.SetTitle(title);
        
        public void SetMessageText(string message) => _view.SetMessageText(message);

        public override void Dispose()
        {
            base.Dispose();

            _view.ButtonClicked -= OnButtonClicked;
        }

        protected override void OnPreShow()
        {
            base.OnPreShow();
        }

        protected override void OnPreHide()
        {
            base.OnPreHide();

            _view.ButtonClicked -= OnButtonClicked;
        }

        private void OnButtonClicked()
        {
            _coroutinesPerformer.StartPerform(_sceneSwitcher.ProcessSwitchTo(Scenes.MainMenu));
            OnCloseRequest();
        }
    }
}
