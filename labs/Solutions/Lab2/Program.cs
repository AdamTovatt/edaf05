using Common;
using Common.DataSources;
using Lab2.Models;
using Lab2.HashTables;

namespace Lab2
{
    public class Program
    {
        static void Main(string[] args)
        {
            IInputDataSource inputSource = new ConsoleInputDataSource();
            IHashTable<string, int> hashTable = new LinearProbingHashTable<string, int>();

            ProcessInput(inputSource, hashTable);
        }

        public static void ProcessInput(IInputDataSource inputSource, IHashTable<string, int> hashTable)
        {
            InputDataReader<InputData> reader = new InputDataReader<InputData>(inputSource);
            InputData input = reader.Read();

            int i = 0;

            foreach (string word in input.Words)
            {
                bool removeIt = i % 16 == 0;

                if (hashTable.TryGet(word, out int current))
                {
                    if (removeIt)
                    {
                        hashTable.Remove(word);
                    }
                    else
                    {
                        hashTable.Put(word, current + 1);
                    }
                }
                else if (!removeIt)
                {
                    hashTable.Put(word, 1);
                }

                i++;
            }

            string resultWord = "";
            int maxCount = -1;

            foreach (KeyValuePair<string, int> pair in hashTable.GetPairs())
            {
                if (pair.Value > maxCount || (pair.Value == maxCount && string.Compare(pair.Key, resultWord, StringComparison.Ordinal) < 0))
                {
                    resultWord = pair.Key;
                    maxCount = pair.Value;
                }
            }

            Console.WriteLine($"{resultWord} {maxCount}");
        }
    }
}
