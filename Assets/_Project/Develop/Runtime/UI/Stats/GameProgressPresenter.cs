using System.Collections.Generic;
using Assets._Project.Develop.Runtime.Meta.Features.Stats;
using Assets._Project.Develop.Runtime.Meta.Features.Wallet;
using Assets._Project.Develop.Runtime.UI.CommonViews;
using Assets._Project.Develop.Runtime.UI.Core;

namespace Assets._Project.Develop.Runtime.UI.Stats
{
    public class GameProgressPresenter : IPresenter
    {
        private readonly GameplayProgressService _progressService;
        private readonly ProjectPresentersFactory _presentersFactory;
        private readonly ViewsFactory _viewsFactory;
        private readonly IconTextListView _view;
        private readonly List<StatPresenter> _statPresenters = new();

        public GameProgressPresenter(
            GameplayProgressService progressService,
            ProjectPresentersFactory presentersFactory,
            ViewsFactory viewsFactory,
            IconTextListView view)
        {
            _progressService = progressService;
            _presentersFactory = presentersFactory;
            _viewsFactory = viewsFactory;
            _view = view;
        }

        public void Initialize()
        {
            foreach (StatTypes statType in _progressService.AvailableStats)
            {
                IconTextView statView = _viewsFactory.Create<IconTextView>(ViewIDs.CurrencyView);
                _view.Add(statView);

                StatPresenter statPresenter = _presentersFactory.CreateStatPresenter(
                    statView,
                    _progressService.GetStat(statType),
                    statType);

                statPresenter.Initialize();
                _statPresenters.Add(statPresenter);
            }
        }

        public void Dispose()
        {
            foreach (StatPresenter statPresenter in _statPresenters)
            {
                _view.Remove(statPresenter.View);
                _viewsFactory.Release(statPresenter.View);
                statPresenter.Dispose();
            }

            _statPresenters.Clear();
        }
    }
}
