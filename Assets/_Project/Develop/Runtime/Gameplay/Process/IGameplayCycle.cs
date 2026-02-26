using Assets._Project.Develop.Runtime.Configs.Gameplay.Levels;

namespace Assets._Project.Develop.Runtime.Gameplay.Process
{
    public interface IGameplayCycle
    {
        void Run(LevelConfig config);
        void Update();
    }
}
