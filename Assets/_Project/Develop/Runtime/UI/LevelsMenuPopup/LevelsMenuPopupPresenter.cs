using Assets._Project.Develop.Runtime.Configs.Gameplay.Levels;
using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;

namespace Assets._Project.Develop.Runtime.UI.LevelsMenuPopup
{
    public class LevelsMenuPopupPresenter : PopupPresenterBase
    {
        private const string TitleName = "LEVELS";

        private readonly ConfigsProviderService _configProviderService;
        private readonly ProjectPresentersFactory _presentersFactory;
        private readonly ViewsFactory _viewsFactory;
        private readonly LevelsMenuPopupView _view;

        public LevelsMenuPopupPresenter(
            ICoroutinesPerformer coroutinesPerformer,
            ConfigsProviderService configProviderService,
            ProjectPresentersFactory presentersFactory,
            ViewsFactory viewsFactory,
            LevelsMenuPopupView view) : base(coroutinesPerformer)
        {
            _configProviderService = configProviderService;
            _presentersFactory = presentersFactory;
            _viewsFactory = viewsFactory;
            _view = view;
        }

        protected override PopupViewBase PopupView => _view;

        public override void Initialize()
        {
            base.Initialize();

            _view.SetTitle(TitleName);

            LevelsListConfig levelsListConfig = _configProviderService.GetConfig<LevelsListConfig>();

            for (int i = 0; i < levelsListConfig.Levels.Count; i++)
            {
                LevelTileView levelTileView = _viewsFactory.Create<LevelTileView>(ViewIDs.LevelTile);

                _view.LevelsListView.Add(levelTileView);
            }
        }
    }
}
