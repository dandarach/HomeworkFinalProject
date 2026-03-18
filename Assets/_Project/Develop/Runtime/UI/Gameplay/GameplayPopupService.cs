using Assets._Project.Develop.Runtime.UI.Core;
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
    }
}
