using UnityEngine;
using System;
using Assets._Project.Develop.Runtime.Gameplay.Features.StringServices;
using Assets._Project.Develop.Runtime.UI.CommonViews;
using Assets._Project.Develop.Runtime.UI.Core;

namespace Assets._Project.Develop.Runtime.UI.Gameplay.Features.StringServices
{
    public class StringValidatorPresenter : IPresenter
    {
        private readonly StringValidator _stringValidator;
        private readonly TextView _view;
        private IDisposable _disposable;

        public StringValidatorPresenter(
            StringValidator stringValidator,
            TextView view)
        {
            _stringValidator = stringValidator;
            _view = view;
        }

        public void Initialize()
        {
            UpdateValue(_stringValidator.InputString.Value);
            _disposable = _stringValidator.InputString.Subscribe(OnStringChanged);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }

        private void OnStringChanged(string arg1, string newValue) => UpdateValue(newValue);

        private void UpdateValue(string value) => _view.SetText(value);
    }
}
