namespace Lab2.HashTables
{
    public class SeparateChainingHashTable<TKey, TValue> : IConfigurableHashTable<TKey, TValue>
        where TKey : notnull
    {
        private class Entry
        {
            public TKey Key { get; }
            public TValue Value { get; set; }

            public Entry(TKey key, TValue value)
            {
                Key = key;
                Value = value;
            }
        }

        private List<Entry>[] _buckets;
        private int _count;

        public int ResizeCount { get; private set; } = 0;
        public int BucketCount => _buckets.Length;
        public double MaxLoadFactor { get; set; } = 0.75; // Resize up if count exceeds this threshold
        public double MinLoadFactor { get; set; } = 0.25; // Resize down if count drops below this
        private const int MinBucketCount = 1;

        public SeparateChainingHashTable()
        {
            _buckets = new List<Entry>[MinBucketCount];
        }

        public bool TryGet(TKey key, out TValue value)
        {
            int index = GetIndex(key);
            List<Entry>? bucket = _buckets[index];

            if (bucket != null)
            {
                foreach (Entry entry in bucket)
                {
                    if (entry.Key.Equals(key))
                    {
                        value = entry.Value;
                        return true;
                    }
                }
            }

            value = default!;
            return false;
        }

        public void Put(TKey key, TValue value)
        {
            int index = GetIndex(key);
            _buckets[index] ??= new List<Entry>();

            foreach (Entry entry in _buckets[index])
            {
                if (entry.Key.Equals(key))
                {
                    entry.Value = value;
                    return;
                }
            }

            _buckets[index].Add(new Entry(key, value));
            _count++;

            // Double the number of buckets if load factor is too high
            if (_count > _buckets.Length * MaxLoadFactor)
                Resize(_buckets.Length * 2);
        }

        public bool Remove(TKey key)
        {
            int index = GetIndex(key);
            List<Entry>? bucket = _buckets[index];

            if (bucket == null)
                return false;

            for (int i = 0; i < bucket.Count; i++)
            {
                if (bucket[i].Key.Equals(key))
                {
                    bucket.RemoveAt(i);
                    _count--;

                    // Resize down to half if load factor is too low and size is above minimum
                    int shrinkThreshold = (int)(_buckets.Length * MinLoadFactor);
                    if (_buckets.Length > MinBucketCount && _count < shrinkThreshold)
                        Resize(_buckets.Length / 2);

                    return true;
                }
            }

            return false;
        }

        public IEnumerable<KeyValuePair<TKey, TValue>> GetPairs()
        {
            foreach (List<Entry>? bucket in _buckets)
            {
                if (bucket == null) continue;

                foreach (Entry entry in bucket)
                    yield return new KeyValuePair<TKey, TValue>(entry.Key, entry.Value);
            }
        }

        private int GetIndex(TKey key)
        {
            return key.GetKeyHash() % _buckets.Length;
        }

        private void Resize(int newSize)
        {
            if (newSize < MinBucketCount)
                newSize = MinBucketCount;

            ResizeCount++; // Track how often resizing occurs

            List<Entry>[] newBuckets = new List<Entry>[newSize];

            foreach (KeyValuePair<TKey, TValue> pair in GetPairs())
            {
                int index = pair.Key.GetKeyHash() % newSize;
                newBuckets[index] ??= new List<Entry>();
                newBuckets[index].Add(new Entry(pair.Key, pair.Value));
            }

            _buckets = newBuckets;
        }
    }
}
