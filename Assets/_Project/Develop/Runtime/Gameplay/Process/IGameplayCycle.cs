using Assets._Project.Develop.Runtime.Meta.Configs;

namespace Assets._Project.Develop.Runtime.Gameplay.Process
{
    public interface IGameplayCycle
    {
        void Run(LevelConfig config);
        void Update();
    }
}
