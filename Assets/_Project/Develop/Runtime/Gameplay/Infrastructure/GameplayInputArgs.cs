using Assets._Project.Develop.Runtime.Configs.Gameplay;
using Assets._Project.Develop.Runtime.Utilities.SceneManagement;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayInputArgs : IInputSceneArgs
    {
        public GameplayInputArgs(LevelConfig config)
        {
            Symbols = config.Symbols;
            SymbolsToGuess = config.SymbolsToGuess;
        }

        public string Symbols { get; }
        public int SymbolsToGuess { get; }
    }
}
