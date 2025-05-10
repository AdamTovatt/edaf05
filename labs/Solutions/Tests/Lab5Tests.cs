using Common;
using Common.DataSources;
using Lab5;
using Lab5.Models;
using Tests.Helpers;

namespace Tests
{
    [TestClass]
    public class Lab5Tests
    {
        private const string basePath = "..\\..\\..\\TestData\\Lab5";

        [DataTestMethod]
        [DataRow("sample", "1")]
        [DataRow("secret", "0mini")]
        [DataRow("secret", "1small")]
        [DataRow("secret", "2med")]
        [DataRow("secret", "3large")]
        [DataRow("secret", "4huge")]
        public void Solve_ReturnsCorrectAnswer(string subdirectory, string fileName)
        {
            TestCasePath path = TestCasePath.From(basePath, subdirectory, fileName);
            RunTestWithPaths(path);
        }

        private void RunTestWithPaths(TestCasePath path)
        {
            using FileInputDataSource inputSource = new FileInputDataSource(path.InputPath);
            using StreamReader expectedOutputReader = new StreamReader(path.AnswerPath);

            SectionTimer sectionTimer = new SectionTimer();

            // Read and cache input for both solving and validation
            InputDataReader<InputData> inputReader = new InputDataReader<InputData>(inputSource);
            InputData input = inputReader.Read();

            // Run solver with new input source (required by Program.Solve signature)
            using FileInputDataSource freshSource = new FileInputDataSource(path.InputPath);
            List<StringPair> actualResults = Program.Solve(freshSource, sectionTimer).ToList();
            List<StringPair> expectedResults = ReadExpectedResults(expectedOutputReader).ToList();            

            Assert.AreEqual(expectedResults.Count, actualResults.Count, "Mismatch in number of results");

            for (int i = 0; i < actualResults.Count; i++)
            {
                bool valid = AlignmentValidator.IsValidAlignment(
                    input.Queries[i],
                    actualResults[i],
                    expectedResults[i],
                    input.Context,
                    out string? error);

                Assert.IsTrue(valid, $"Query {i + 1} failed: {error}");
            }

            Console.WriteLine(sectionTimer.ToString());
        }

        private IEnumerable<StringPair> ReadExpectedResults(StreamReader reader)
        {
            while (true)
            {
                string? line = reader.ReadLine();
                if (line == null)
                    yield break;

                int spaceIndex = line.IndexOf(' ');
                if (spaceIndex == -1)
                    throw new InvalidOperationException("Malformed expected output line.");

                string first = line.Substring(0, spaceIndex);
                string second = line.Substring(spaceIndex + 1);
                yield return new StringPair(first, second);
            }
        }
    }
}
