using Assets._Project.Develop.Runtime.UI.Core;

namespace Assets._Project.Develop.Runtime.UI.MainMenu
{
    public class MainMenuScreenPresenter : IPresenter
    {
        private readonly MainMenuScreenView _screen;

        public MainMenuScreenPresenter(MainMenuScreenView screen)
        {
            _screen = screen;
        }

        public void Initialize()
        {
        }

        public void Dispose()
        {
        }
    }
}
