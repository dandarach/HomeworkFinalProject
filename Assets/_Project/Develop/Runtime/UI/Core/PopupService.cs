using Assets._Project.Develop.Runtime.UI.Core.TestPopup;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.UI.Core
{
    public abstract class PopupService
    {
        protected readonly ViewsFactory ViewsFactory;

        private readonly ProjectPresentersFactory _presentersFactory;

        protected PopupService(
            ViewsFactory viewsFactory,
            ProjectPresentersFactory presentersFactory)
        {
            ViewsFactory = viewsFactory;
            _presentersFactory = presentersFactory;
        }

        protected abstract Transform PopupLayer {  get; }

        public TestPopupPresenter OpenTestPopup()
        {
            TestPopupView view = ViewsFactory.Create<TestPopupView>(ViewIDs.TestPopup, PopupLayer);

            TestPopupPresenter popup = _presentersFactory.CreateTestPopupPresenter(view);

            popup.Initialize();
            popup.Show();

            return popup;
        }
    }
}
