using Assets._Project.Develop.Runtime.Gameplay.Infrastructure;
using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;

namespace Assets._Project.Develop.Runtime.UI.Gameplay.Popups
{
    public class DefeatPopupPresenter : PopupPresenterBase
    {
        private const string TitleName = "GAME OVER";
        private const string DefeatMessage = "DEFEAT!";

        private readonly DefeatPopupView _view;
        private readonly SceneSwitcherService _sceneSwitcher;
        private readonly ICoroutinesPerformer _coroutinesPerformer;

        public DefeatPopupPresenter(
            ICoroutinesPerformer coroutinesPerformer,
            DefeatPopupView view,
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
            _view.SetMessageText(DefeatMessage);

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
            //_coroutinesPerformer.StartPerform(
            //    _sceneSwitcher.ProcessSwitchTo(
            //        Scenes.Gameplay,
            //        new GameplayInputArgs(_currentLevelArgs.GameplayMode)));

            OnCloseRequest();
        }
    }
}
