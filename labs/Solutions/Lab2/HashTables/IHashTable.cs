public interface IHashTable<TKey, TValue>
{
    bool TryGet(TKey key, out TValue value);
    void Put(TKey key, TValue value);
    bool Remove(TKey key);
    IEnumerable<KeyValuePair<TKey, TValue>> GetPairs();
}
