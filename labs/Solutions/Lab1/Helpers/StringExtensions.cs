namespace Lab1.Helpers
{
    public static class StringExtensions
    {
        public static Dictionary<char, int> GetCharacterCountDictionary(this string word)
        {
            Dictionary<char, int> counts = new Dictionary<char, int>(); // count of each character

            foreach (char character in word)
            {
                if (!counts.ContainsKey(character))
                    counts[character] = 0;

                counts[character]++;
            }

            return counts;
        }

        public static bool ContainsAll(this string word, Dictionary<char, int> requiredCharacterCounts)
        {
            Dictionary<char, int> characterCounts = GetCharacterCountDictionary(word); // count of each character in the word to check

            foreach (KeyValuePair<char, int> requiredCount in requiredCharacterCounts) // go through the required counts
            {
                if (!characterCounts.TryGetValue(requiredCount.Key, out int count) || count < requiredCount.Value)
                    return false; // if the count for the current character doesn't exist or is less than the required we return false
            }

            return true;
        }
    }
}
