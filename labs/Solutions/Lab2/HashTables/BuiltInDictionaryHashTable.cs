namespace Lab2.HashTables
{
    public class BuiltInDictionaryHashTable<TKey, TValue> : IHashTable<TKey, TValue>
        where TKey : notnull
    {
        private readonly Dictionary<TKey, TValue> _dict = new Dictionary<TKey, TValue>();

        public bool TryGet(TKey key, out TValue value)
        {
            return _dict.TryGetValue(key, out value!);
        }

        public void Put(TKey key, TValue value)
        {
            _dict[key] = value;
        }

        public bool Remove(TKey key)
        {
            return _dict.Remove(key);
        }

        public IEnumerable<KeyValuePair<TKey, TValue>> GetPairs()
        {
            return _dict;
        }
    }
}
