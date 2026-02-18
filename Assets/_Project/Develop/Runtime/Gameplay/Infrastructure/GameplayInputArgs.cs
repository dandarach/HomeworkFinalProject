using Assets._Project.Develop.Runtime.Utilities.SceneManagement;

namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class GameplayInputArgs : IInputSceneArgs
    {
        public GameplayInputArgs(string symbols, int symbolsCount)
        {
            Symbols = symbols;
            SymbolsCount = symbolsCount;
        }

        public string Symbols { get; }
        public int SymbolsCount { get; }
    }
}
