namespace Lab2.HashTables
{
    public static class HashTableFactory
    {
        public static IConfigurableHashTable<string, int> Create(Type type, double maxLoad, double minLoad)
        {
            if (Activator.CreateInstance(type) is not IConfigurableHashTable<string, int> instance)
                throw new ArgumentException($"Type {type.Name} is not a valid hash table");

            instance.MaxLoadFactor = maxLoad;
            instance.MinLoadFactor = minLoad;
            return instance;
        }
    }
}
