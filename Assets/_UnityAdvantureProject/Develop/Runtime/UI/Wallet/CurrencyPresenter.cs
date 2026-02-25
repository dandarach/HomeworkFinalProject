using System;
using System.Collections.Generic;
using Assets._UnityAdvantureProject.Develop.Runtime.Configs.Meta.Wallet;
using Assets._UnityAdvantureProject.Develop.Runtime.Meta.Features.Wallet;
using Assets._UnityAdvantureProject.Develop.Runtime.UI.CommonViews;
using Assets._UnityAdvantureProject.Develop.Runtime.Utilities.Reactive;

namespace Assets._UnityAdvantureProject.Develop.Runtime.UI.Wallet
{
    public class CurrencyPresenter
    {
        //business logic
        private readonly IReadonlyVariable<int> _currency;
        private readonly CurrencyTypes _currencyType;
        private readonly CurrencyIconsConfig _currencyIconsConfig;

        //visual
        private readonly IconTextView _view;

        private IDisposable _disposable;

        public CurrencyPresenter(
            IReadonlyVariable<int> currency,
            CurrencyTypes currencyType,
            CurrencyIconsConfig currencyIconsConfig,
            IconTextView view)
        {
            _currency = currency;
            _currencyType = currencyType;
            _currencyIconsConfig = currencyIconsConfig;
            _view = view;
        }

        public void Enable()
        {
            UpdateValue(_currency.Value);
            _view.SetIcon(_currencyIconsConfig.GetSpriteFor(_currencyType));

            _disposable = _currency.Subscribe(OnCurrencyChanged);
        }

        public void Disable()
        {
            _disposable.Dispose();
        }

        private void OnCurrencyChanged(int arg1, int newValue) => UpdateValue(newValue);

        private void UpdateValue(int value) => _view.SetText(value.ToString());
    }
}
