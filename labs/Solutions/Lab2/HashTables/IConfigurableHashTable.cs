namespace Lab2.HashTables
{
    public interface IConfigurableHashTable<TKey, TValue> : IInspectableHashTable<TKey, TValue>
    {
        double MaxLoadFactor { get; set; }
        double MinLoadFactor { get; set; }
    }
}
