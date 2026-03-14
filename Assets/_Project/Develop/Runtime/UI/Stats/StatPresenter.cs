using System;
using Assets._Project.Develop.Runtime.Configs.Meta.Stats;
using Assets._Project.Develop.Runtime.Meta.Features.Stats;
using Assets._Project.Develop.Runtime.UI.CommonViews;
using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.Utilities.Reactive;

namespace Assets._Project.Develop.Runtime.UI.Stats
{
    public class StatPresenter : IPresenter
    {
        //business logic
        private readonly IReadonlyVariable<int> _stat;
        private readonly StatTypes _statType;
        private readonly StatIconsConfig _statIconsConfig;

        //visual
        private readonly IconTextView _view;

        private IDisposable _disposable;

        public StatPresenter(
            IReadonlyVariable<int> stat,
            StatTypes statType,
            StatIconsConfig statIconsConfig,
            IconTextView view)
        {
            _stat = stat;
            _statType = statType;
            _statIconsConfig = statIconsConfig;
            _view = view;
        }

        public IconTextView View => _view;

        public void Initialize()
        {
            UpdateValue(_stat.Value);
            _view.SetIcon(_statIconsConfig.GetSpriteFor(_statType));

            _disposable = _stat.Subscribe(OnStatChanged);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }

        private void OnStatChanged(int arg1, int newValue) => UpdateValue(newValue);

        private void UpdateValue(int value) => _view.SetText(value.ToString());
    }
}
