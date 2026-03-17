using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;

namespace Assets._Project.Develop.Runtime.UI.Gameplay.Popups
{
    public class GameplayPopupPresenter : PopupPresenterBase
    {
        private const string TitleName = "LEVELS";

        private readonly ProjectPresentersFactory _presentersFactory;
        private readonly ViewsFactory _viewsFactory;
        private readonly GameplayPopupView _view;

        public GameplayPopupPresenter(
            ICoroutinesPerformer coroutinesPerformer,
            ProjectPresentersFactory presentersFactory,
            ViewsFactory viewsFactory,
            GameplayPopupView view) : base(coroutinesPerformer)
        {
            _presentersFactory = presentersFactory;
            _viewsFactory = viewsFactory;
            _view = view;
        }

        protected override PopupViewBase PopupView => _view;

        public override void Initialize()
        {
            base.Initialize();

            _view.SetTitle(TitleName);

        }

        public override void Dispose()
        {
            base.Dispose();
        }

        protected override void OnPreShow()
        {
            base.OnPreShow();
        }

        protected override void OnPreHide()
        {
            base.OnPreHide();
        }
    }
}
