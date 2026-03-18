using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;

namespace Assets._Project.Develop.Runtime.UI.Gameplay.Popups
{
    public class GameplayPopupPresenter : PopupPresenterBase
    {
        private const string TitleName = "LEVELS";

        private readonly GameplayPopupView _view;

        public GameplayPopupPresenter(
            ICoroutinesPerformer coroutinesPerformer,
            GameplayPopupView view) : base(coroutinesPerformer)
        {
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
