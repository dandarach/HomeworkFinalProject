using Assets._Project.Develop.Runtime.Meta.Configs;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayInputArgs : IInputSceneArgs
    {
        public GameplayInputArgs(GameplayMode gameMode)
        {
            GameplayMode = gameMode;
        }

        public GameplayMode GameplayMode { get; }
    }
}
