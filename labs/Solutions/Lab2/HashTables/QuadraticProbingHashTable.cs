namespace Lab2.HashTables
{
    public class QuadraticProbingHashTable<TKey, TValue> : IConfigurableHashTable<TKey, TValue>
        where TKey : notnull
    {
        private enum EntryState { Empty, Occupied, Deleted }

        private struct Entry
        {
            public TKey Key;
            public TValue Value;
            public EntryState State;
        }

        private Entry[] _entries;
        private int _count;

        public int ResizeCount { get; private set; } = 0;
        public int BucketCount => _entries.Length;
        public double MaxLoadFactor { get; set; } = 0.5;  // must stay <= 0.5 for quadratic probing
        public double MinLoadFactor { get; set; } = 0.25;
        private const int MinBucketCount = 1;

        public QuadraticProbingHashTable()
        {
            _entries = new Entry[MinBucketCount];
        }

        public bool TryGet(TKey key, out TValue value)
        {
            int index = FindIndex(key, out bool found);
            if (found)
            {
                value = _entries[index].Value;
                return true;
            }

            value = default!;
            return false;
        }

        public void Put(TKey key, TValue value)
        {
            if ((_count + 1) > _entries.Length * MaxLoadFactor)
                Resize(_entries.Length * 2);

            int hash = key.GetKeyHash();
            int tableSize = _entries.Length;
            int firstDeletedIndex = -1;

            for (int i = 0; i < tableSize; i++)
            {
                int index = (hash + i * i) % tableSize;
                EntryState state = _entries[index].State;

                if (state == EntryState.Occupied && _entries[index].Key!.Equals(key))
                {
                    _entries[index].Value = value;
                    return;
                }

                if (state == EntryState.Deleted && firstDeletedIndex == -1)
                    firstDeletedIndex = index;

                if (state == EntryState.Empty)
                {
                    int insertIndex = firstDeletedIndex != -1 ? firstDeletedIndex : index;
                    _entries[insertIndex] = new Entry
                    {
                        Key = key,
                        Value = value,
                        State = EntryState.Occupied
                    };
                    _count++;
                    return;
                }
            }

            throw new InvalidOperationException($"Hash table is full or probing failed (Count: {_count}, Capacity: {_entries.Length})");
        }

        public bool Remove(TKey key)
        {
            int index = FindIndex(key, out bool found);
            if (found)
            {
                _entries[index].State = EntryState.Deleted;
                _count--;

                if (_entries.Length > MinBucketCount && _count < _entries.Length * MinLoadFactor)
                    Resize(_entries.Length / 2);

                return true;
            }

            return false;
        }

        public IEnumerable<KeyValuePair<TKey, TValue>> GetPairs()
        {
            foreach (Entry entry in _entries)
            {
                if (entry.State == EntryState.Occupied)
                    yield return new KeyValuePair<TKey, TValue>(entry.Key, entry.Value);
            }
        }

        private int FindIndex(TKey key, out bool found)
        {
            int hash = key.GetKeyHash();
            int tableSize = _entries.Length;

            for (int i = 0; i < tableSize; i++)
            {
                int index = (hash + i * i) % tableSize;

                if (_entries[index].State == EntryState.Empty)
                    break;

                if (_entries[index].State == EntryState.Occupied && _entries[index].Key!.Equals(key))
                {
                    found = true;
                    return index;
                }
            }

            found = false;
            return -1;
        }

        private void Resize(int newSize)
        {
            if (newSize < MinBucketCount)
                newSize = MinBucketCount;

            ResizeCount++;

            Entry[] oldEntries = _entries;
            _entries = new Entry[newSize];
            _count = 0;

            foreach (Entry entry in oldEntries)
            {
                if (entry.State == EntryState.Occupied)
                    Put(entry.Key, entry.Value);
            }
        }
    }
}
