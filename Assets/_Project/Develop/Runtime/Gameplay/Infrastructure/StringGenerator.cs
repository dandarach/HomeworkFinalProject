namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class StringGenerator
    {
        public string Generate(string symbols, int length)
        {
            char[] characters = symbols.ToCharArray();
            string randomString = "";

            for (int i = 0; i < length; i++)
            {
                int index = UnityEngine.Random.Range(0, characters.Length);
                randomString += characters[index];
            }

            return randomString;
        }
    }
}
