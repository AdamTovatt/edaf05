namespace Lab2.HashTables
{
    public class LinearProbingHashTable<TKey, TValue> : IConfigurableHashTable<TKey, TValue>
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
        public double MaxLoadFactor { get; set; } = 0.75;
        public double MinLoadFactor { get; set; } = 0.25;
        private const int MinBucketCount = 1;

        public LinearProbingHashTable()
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

            int index = key.GetKeyHash() % _entries.Length;
            int firstDeletedIndex = -1;

            while (_entries[index].State != EntryState.Empty)
            {
                if (_entries[index].State == EntryState.Occupied && _entries[index].Key!.Equals(key))
                {
                    _entries[index].Value = value;
                    return;
                }

                if (_entries[index].State == EntryState.Deleted && firstDeletedIndex == -1)
                {
                    firstDeletedIndex = index;
                }

                index = (index + 1) % _entries.Length;
            }

            // Insert into the first deleted slot found if any, else the current slot
            int insertIndex = firstDeletedIndex != -1 ? firstDeletedIndex : index; // This was a bit tricky

            _entries[insertIndex] = new Entry
            {
                Key = key,
                Value = value,
                State = EntryState.Occupied
            };

            _count++;
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
            int startIndex = key.GetKeyHash() % _entries.Length;
            int index = startIndex;

            while (_entries[index].State != EntryState.Empty)
            {
                if (_entries[index].State == EntryState.Occupied && _entries[index].Key!.Equals(key))
                {
                    found = true;
                    return index;
                }

                index = (index + 1) % _entries.Length;

                if (index == startIndex)
                    break; // full cycle, key not found
            }

            found = false;
            return index; // returns position to insert if needed
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
                {
                    Put(entry.Key, entry.Value);
                }
            }
        }
    }
}
