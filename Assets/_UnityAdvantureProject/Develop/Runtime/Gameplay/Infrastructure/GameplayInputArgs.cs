using Assets._UnityAdvantureProject.Develop.Runtime.Utilities.SceneManagement;

namespace Assets._UnityAdvantureProject.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayInputArgs : IInputSceneArgs
    {
        public GameplayInputArgs(int levelNumber)
        {
            LevelNumber = levelNumber;
        }

        public int LevelNumber { get; }
    }
}
