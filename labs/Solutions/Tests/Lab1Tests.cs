using Lab1.Helpers;
using Lab1.Helpers.DataSources;
using Lab1.Models;
using Tests.Helpers;

namespace Tests
{
    [TestClass]
    public class Lab1Tests
    {
        private const string basePath = "..\\..\\..\\TestData\\Lab1";

        [DataTestMethod]
        [DataRow("sample", "1")]
        [DataRow("secret", "1small1")]
        [DataRow("secret", "2small2")]
        [DataRow("secret", "3medium1")]
        [DataRow("secret", "4medium2")]
        [DataRow("secret", "5large1")]
        [DataRow("secret", "6large2")]
        public void SolveCases(string subdir, string fileName)
        {
            TestCasePath path = TestCasePath.From(basePath, subdir, fileName);
            TestSolverWithPaths(path);
        }

        private void TestSolverWithPaths(TestCasePath path)
        {
            using FileInputDataSource dataSource = new FileInputDataSource(path.InputPath);
            InputDataReader reader = new InputDataReader(dataSource);
            InputData input = reader.ReadInput();

            Graph graph = Graph.Create(input.Words);

            using StreamReader expectedOutputReader = new StreamReader(path.AnswerPath);
            foreach (Query query in input.Queries)
            {
                Node start = graph.GetNode(query.From);
                Node end = graph.GetNode(query.To);

                int? result = PathFinder.FindShortestPath(start, end);
                string? expectedLine = expectedOutputReader.ReadLine();

                Assert.IsNotNull(expectedLine);

                string actual = result.HasValue ? result.Value.ToString() : "Impossible";
                Assert.AreEqual(expectedLine, actual);
            }
        }
    }
}
