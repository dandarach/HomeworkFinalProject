namespace Assets._Project.Develop.Runtime.UI.Core
{
    public interface ISubscibedPresenter : IPresenter
    {
        void Subscribe();

        void Unsubscribe();
    }
}
