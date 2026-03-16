using Assets._Project.Develop.Runtime.Utilities.Reactive;

namespace Assets._Project.Develop.Runtime.Gameplay.Features.StringServices
{
    public class StringGenerator
    {
        public ReactiveVariable<string> GeneratedString { get; private set; }

        public void Generate(string symbols, int length)
        {
            char[] characters = symbols.ToCharArray();
            string GeneratedString = "";

            for (int i = 0; i < length; i++)
            {
                int index = UnityEngine.Random.Range(0, characters.Length);
                GeneratedString += characters[index];
            }
        }
    }
}
