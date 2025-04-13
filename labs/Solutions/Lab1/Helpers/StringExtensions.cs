namespace Lab1.Helpers
{
    public static class StringExtensions
    {
        public static Dictionary<char, int> GetCharacterCountDictionary(this string word)
        {
            Dictionary<char, int> counts = new Dictionary<char, int>();

            foreach (char character in word)
            {
                if (!counts.ContainsKey(character))
                    counts[character] = 0;

                counts[character]++;
            }

            return counts;
        }

        public static bool ContainsAll(this string word, Dictionary<char, int> required)
        {
            Dictionary<char, int> wordCounts = GetCharacterCountDictionary(word);

            foreach (KeyValuePair<char, int> keyValuePair in required)
            {
                if (!wordCounts.TryGetValue(keyValuePair.Key, out int count) || count < keyValuePair.Value)
                    return false;
            }
            return true;
        }
    }
}
