using System;
using Assets._Project.Develop.Runtime.Gameplay.Features.StringServices;
using Assets._Project.Develop.Runtime.UI.CommonViews;
using Assets._Project.Develop.Runtime.UI.Core;

namespace Assets._Project.Develop.Runtime.UI.Gameplay.Features.StringServices
{
    public class StringGeneratorPresenter : IPresenter
    {
        private readonly StringGenerator _stringGenerator;
        private readonly TextView _view;
        private IDisposable _disposable;

        public StringGeneratorPresenter(
            StringGenerator stringGenerator,
            TextView view)
        {
            _stringGenerator = stringGenerator;
            _view = view;
        }

        public void Initialize()
        {
            UpdateValue(_stringGenerator.GeneratedString.Value);
            _disposable = _stringGenerator.GeneratedString.Subscribe(OnStringChanged);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }

        private void OnStringChanged(string arg1, string newValue) => UpdateValue(newValue);

        private void UpdateValue(string value) => _view.SetText(value);
    }
}
