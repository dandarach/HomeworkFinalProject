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
        
        public GameplayPopupPresenter OpenPopup(string title, string message, Action Callback = null)
        {
            GameplayPopupView view = ViewsFactory.Create<GameplayPopupView>(ViewIDs.GameplayPopup, PopupLayer);
            view.SetTitle(title);
            view.SetMessageText(message);

            GameplayPopupPresenter popup = _presentersFactory.CreateGameplayPopupPresenter(view);

            OnPopupCreated(popup, view, Callback);

            return popup;
        }
    }
}
