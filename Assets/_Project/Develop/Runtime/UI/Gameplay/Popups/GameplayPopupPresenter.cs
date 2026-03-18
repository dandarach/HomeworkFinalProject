using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;

namespace Assets._Project.Develop.Runtime.UI.Gameplay.Popups
{
    public class GameplayPopupPresenter : PopupPresenterBase
    {
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
        }

        public void SetTitle(string title) => _view.SetTitle(title);
        
        public void SetMessageText(string message) => _view.SetMessageText(message);

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
