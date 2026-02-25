using Assets._Project.Develop.Runtime.Gameplay.Infrastructure;

namespace Assets._Project.Develop.Runtime.Gameplay.Process
{
    public interface IGameplayCycle
    {
        void Run(GameplayInputArgs args);
        void Update();
    }
}
