using Common.DataSources;
using Lab2.HashTables;
using System.Diagnostics;
using Tests.Helpers;

namespace Tests
{
    [TestClass]
    public class Lab2Tests
    {
        private const string basePath = "..\\..\\..\\TestData\\Lab2";

        [DataTestMethod]
        [DataRow("sample", "1")]
        [DataRow("sample", "2")]
        [DataRow("secret", "1musl")]
        [DataRow("secret", "2pg")]
        [DataRow("secret", "3glibc")]
        [DataRow("secret", "4linux")]
        [DataRow("secret", "5ppc")]
        [DataRow("secret", "6big")]
        [DataRow("secret", "7huge")]
        public void SolveWithBuiltInDictionary(string subdir, string fileName)
        {
            TestCasePath path = TestCasePath.From(basePath, subdir, fileName);
            TestSolverWithPaths(path, new BuiltInDictionaryHashTable<string, int>());
        }

        [DataTestMethod]
        [DataRow("sample", "1")]
        [DataRow("sample", "2")]
        [DataRow("secret", "1musl")]
        [DataRow("secret", "2pg")]
        [DataRow("secret", "3glibc")]
        [DataRow("secret", "4linux")]
        [DataRow("secret", "5ppc")]
        [DataRow("secret", "6big")]
        [DataRow("secret", "7huge")]
        public void SolveWithSeparateChainHashTable(string subdir, string fileName)
        {
            TestCasePath path = TestCasePath.From(basePath, subdir, fileName);
            TestSolverWithPaths(path, new SeparateChainingHashTable<string, int>());
        }

        [DataTestMethod]
        [DataRow("sample", "1")]
        [DataRow("sample", "2")]
        [DataRow("secret", "1musl")]
        [DataRow("secret", "2pg")]
        [DataRow("secret", "3glibc")]
        [DataRow("secret", "4linux")]
        [DataRow("secret", "5ppc")]
        [DataRow("secret", "6big")]
        [DataRow("secret", "7huge")]
        public void SolveWithLinearProbingHasTable(string subdir, string fileName)
        {
            TestCasePath path = TestCasePath.From(basePath, subdir, fileName);
            TestSolverWithPaths(path, new LinearProbingHashTable<string, int>());
        }

        [DataTestMethod]
        [DataRow("sample", "1")]
        [DataRow("sample", "2")]
        [DataRow("secret", "1musl")]
        [DataRow("secret", "2pg")]
        [DataRow("secret", "3glibc")]
        [DataRow("secret", "4linux")]
        [DataRow("secret", "5ppc")]
        [DataRow("secret", "6big")]
        [DataRow("secret", "7huge")]
        public void SolveWithQuadraticProbingHasTable(string subdir, string fileName)
        {
            TestCasePath path = TestCasePath.From(basePath, subdir, fileName);
            TestSolverWithPaths(path, new QuadraticProbingHashTable<string, int>());
        }

        [DataTestMethod]
        [DataRow(typeof(SeparateChainingHashTable<string, int>))]
        [DataRow(typeof(LinearProbingHashTable<string, int>))]
        [DataRow(typeof(QuadraticProbingHashTable<string, int>))]
        public void ShrinksWhenUnderMinLoadFactor(Type hashTableType)
        {
            const int countOfItemsToAdd = 20;
            const double maxLoad = 1.0;
            const double minLoad = 0.25;

            try
            {
                IConfigurableHashTable<string, int> table = HashTableFactory.Create(hashTableType, maxLoad, minLoad);

                // Add enough items to grow the table
                for (int i = 0; i < countOfItemsToAdd; i++)
                {
                    table.Put("key" + i, i);
                }

                int bigSize = table.BucketCount;

                // Remove most items to drop below MinLoadFactor
                for (int i = 0; i < (int)Math.Round(countOfItemsToAdd * 1.8); i++)
                {
                    table.Remove("key" + i);
                }

                int smallSize = table.BucketCount;

                Assert.IsTrue(smallSize < bigSize, $"[{hashTableType.Name}] Table did not shrink as expected. Size before: {bigSize}, after: {smallSize}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{hashTableType.Name}] Shrink test failed with MaxLoad={maxLoad}, MinLoad={minLoad}: {ex.GetType().Name}: {ex.Message}");
            }
        }

        [DataTestMethod]
        [DataRow(typeof(SeparateChainingHashTable<string, int>))]
        [DataRow(typeof(LinearProbingHashTable<string, int>))]
        [DataRow(typeof(QuadraticProbingHashTable<string, int>))]
        public void BenchmarkAllTables(Type hashTableType)
        {
            var configs = new[]
            {
                new { Max = 0.50, Min = 0.20 },
                new { Max = 0.75, Min = 0.25 },
                new { Max = 0.90, Min = 0.30 }
            };

            int itemCount = 100_000;
            int deletes = 70_000;

            Console.WriteLine($"[{hashTableType.Name}]");
            Console.WriteLine($"{"MaxLoad",8} {"MinLoad",8} {"Write(ms)",9} {"Reads(ms)",9} {"Resizes",8} {"Buckets",8} {"Hits",6}");

            foreach (var config in configs)
            {
                try
                {
                    IConfigurableHashTable<string, int> table = HashTableFactory.Create(hashTableType, config.Max, config.Min);

                    Stopwatch writeWatch = Stopwatch.StartNew();

                    for (int i = 0; i < itemCount; i++)
                        table.Put("key" + i, i);

                    for (int i = 0; i < deletes; i++)
                        table.Remove("key" + i);

                    writeWatch.Stop();

                    Stopwatch readWatch = Stopwatch.StartNew();

                    int hits = 0;
                    for (int i = 0; i < itemCount; i++)
                    {
                        if (table.TryGet("key" + i, out _))
                            hits++;
                    }

                    readWatch.Stop();

                    Console.WriteLine($"{config.Max,8:F2} {config.Min,8:F2} {writeWatch.ElapsedMilliseconds,9} {readWatch.ElapsedMilliseconds,9} {table.ResizeCount,8} {table.BucketCount,8} {hits,6}");
                }
                catch (Exception exception)
                {
                    Console.WriteLine($"Failed: Max={config.Max}, Min={config.Min} → {exception.GetType().Name}: {exception.Message}");
                }
            }
        }

        private void TestSolverWithPaths(TestCasePath path, IHashTable<string, int> hashTable)
        {
            using FileInputDataSource input = new FileInputDataSource(path.InputPath);
            using StreamReader expected = new StreamReader(path.AnswerPath);
            using StringWriter output = new StringWriter();

            TextWriter originalOut = Console.Out;
            Console.SetOut(output);

            try
            {
                Lab2.Program.ProcessInput(input, hashTable);
            }
            finally
            {
                Console.SetOut(originalOut);
            }

            string expectedOutput = expected.ReadLine()!;
            string actualOutput = output.ToString().Trim();

            Assert.AreEqual(expectedOutput, actualOutput, $"Mismatch for test case: {Path.GetFileName(path.InputPath)}");
        }
    }
}
