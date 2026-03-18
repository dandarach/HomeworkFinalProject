using Assets._Project.Develop.Runtime.Gameplay.Features.StringServices;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.UI.CommonViews;
using Assets._Project.Develop.Runtime.UI.Core;
using Assets._Project.Develop.Runtime.UI.Gameplay.Features.StringServices;
using Assets._Project.Develop.Runtime.UI.Gameplay.Popups;
using Assets._Project.Develop.Runtime.UI.LevelsMenuPopup;
using Assets._Project.Develop.Runtime.Utilities.ConfigsManagement;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;

namespace Assets._Project.Develop.Runtime.UI.Gameplay
{
    public class GameplayPresentersFactory
    {
        protected readonly DIContainer _container;

        public GameplayPresentersFactory(DIContainer container)
        {
            _container = container;
        }

        public GameplayScreenPresenter CreateGameplayScreen(GameplayScreenView view)
            => new GameplayScreenPresenter(view, this);

        public StringGeneratorPresenter CreateStringGeneratorPresenter(TextView view)
        {
            StringGenerator stringGenerator = _container.Resolve<StringGenerator>();
            return new StringGeneratorPresenter(stringGenerator, view);
        }

        public StringValidatorPresenter CreateStringValidatorPresenter(TextView view)
        {
            StringValidator stringValidator = _container.Resolve<StringValidator>();
            return new StringValidatorPresenter(stringValidator, view);
        }

        public GameplayPopupPresenter CreateGameplayPopupPresenter(GameplayPopupView view)
        {
            return new GameplayPopupPresenter(
                _container.Resolve<ICoroutinesPerformer>(),
                view);
        }
    }
}
