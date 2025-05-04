namespace Lab2
{
    internal static class StringHash
    {
        // like the Musl C library as the assignment suggested
        internal static int KeyHash(string key)
        {
            int h = 0;
            foreach (char c in key)
            {
                h = 31 * h + c;
            }
            return h & 0x7fffffff; // Ensure non-negative
        }

        // just an extension method wrapper for the above hash function
        internal static int GetKeyHash(this string key)
        {
            return KeyHash(key);
        }

        // just an extension method wrapper for the above hash function
        internal static int GetKeyHash(this object key)
        {
            string? stringKey = key as string;

            if (stringKey == null) return 0;

            return stringKey.GetKeyHash();
        }
    }
}
