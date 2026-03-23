using Assets._Project.Develop.Runtime.Gameplay.Features.StringServices;
using Assets._Project.Develop.Runtime.Gameplay.Infrastructure;
using Assets._Project.Develop.Runtime.Infrastructure.DI;
using Assets._Project.Develop.Runtime.UI.CommonViews;
using Assets._Project.Develop.Runtime.UI.Gameplay.Features.StringServices;
using Assets._Project.Develop.Runtime.UI.Gameplay.Popups;
using Assets._Project.Develop.Runtime.Utilities.CoroutinesManagement;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;

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

        public WinPopupPresenter CreateWinPopupPresenter(WinPopupView view)
        {
            return new WinPopupPresenter(
                _container.Resolve<ICoroutinesPerformer>(),
                view,
                _container.Resolve<SceneSwitcherService>());
        }

        public DefeatPopupPresenter CreateDefeatPopupPresenter(DefeatPopupView view)
        {
            return new DefeatPopupPresenter(
                _container.Resolve<ICoroutinesPerformer>(),
                view,
                _container.Resolve<SceneSwitcherService>());
        }
    }
}
