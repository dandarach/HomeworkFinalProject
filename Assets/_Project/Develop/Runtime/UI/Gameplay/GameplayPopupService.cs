using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.UI.Gameplay.Popups;
using Assets._Project.Develop.Runtime.UI.LevelsMenuPopup;
using UnityEngine;

namespace Assets._Project.Develop.Runtime.UI.Gameplay
{
    public class GameplayPopupService : PopupServiceBase
    {
        private readonly GameplayUIRoot _uiRoot;
        private readonly GameplayPresentersFactory _presentersFactory;

        public GameplayPopupService(
            ViewsFactory viewsFactory,
            GameplayPresentersFactory presentersFactory,
            GameplayUIRoot uiRoot)
            : base(viewsFactory)
        {
            _uiRoot = uiRoot;
            _presentersFactory = presentersFactory;
        }

        protected override Transform PopupLayer => _uiRoot.PopupsLayer;
        
        public GameplayPopupPresenter OpenPopup()
        {
            GameplayPopupView view = ViewsFactory.Create<GameplayPopupView>(ViewIDs.GameplayPopup, PopupLayer);

            GameplayPopupPresenter popup = _presentersFactory.CreateGameplayPopupPresenter(view);

            OnPopupCreated(popup, view);

            return popup;
        }
    }
}
