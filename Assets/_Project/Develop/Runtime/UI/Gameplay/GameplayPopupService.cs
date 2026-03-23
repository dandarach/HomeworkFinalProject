using System;
using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.UI.Gameplay.Popups;
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
        
        public WinPopupPresenter OpenWinPopup(Action callback = null)
        {
            WinPopupView view = ViewsFactory.Create<WinPopupView>(ViewIDs.WinPopup, PopupLayer);

            WinPopupPresenter popup = _presentersFactory.CreateWinPopupPresenter(view);

            OnPopupCreated(popup, view, callback);

            return popup;
        }

        public DefeatPopupPresenter OpenDefeatPopup(Action callback = null)
        {
            DefeatPopupView view = ViewsFactory.Create<DefeatPopupView>(ViewIDs.DefeatPopup, PopupLayer);

            DefeatPopupPresenter popup = _presentersFactory.CreateDefeatPopupPresenter(view);

            OnPopupCreated(popup, view, callback);

            return popup;
        }
    }
}
