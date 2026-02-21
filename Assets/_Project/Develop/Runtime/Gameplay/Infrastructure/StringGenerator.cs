namespace Assets._Project.Develop.Runtime.Gameplay.Infrastructure
{
    public class StringGenerator
    {
        private char[] _characters;
        private int _length;

        public StringGenerator(string symbols, int length)
        {
            _characters = symbols.ToCharArray();
            _length = length;
        }

        public string Generate()
        {
            string randomString = "";

            for (int i = 0; i < _length; i++)
            {
                int index = UnityEngine.Random.Range(0, _characters.Length);
                randomString += _characters[index];
            }

            return randomString;
        }
    }
}
