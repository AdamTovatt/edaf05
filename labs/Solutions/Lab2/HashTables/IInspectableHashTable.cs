namespace Lab2.HashTables
{
    public interface IInspectableHashTable<TKey, TValue> : IHashTable<TKey, TValue>
    {
        int ResizeCount { get; }
        int BucketCount { get; }
    }
}
