using Lab1.Helpers;
using Lab1.Helpers.DataSources;
using Lab1.Models;

namespace Tests
{
    [TestClass]
    public class Lab1Tests
    {
        private const string basePath = "..\\..\\..\\TestData\\Lab1\\";

        [TestMethod]
        public void SolveSample()
        {
            const string inputPath = $"{basePath}sample\\1.in";
            const string answerPath = $"{basePath}sample\\1.ans";

            TestSolverWithPaths(inputPath, answerPath);
        }

        [TestMethod]
        public void SolveSecret1Small1()
        {
            const string inputPath = $"{basePath}secret\\1small1.in";
            const string answerPath = $"{basePath}secret\\1small1.ans";

            TestSolverWithPaths(inputPath, answerPath);
        }

        [TestMethod]
        public void SolveSecret2Small2()
        {
            const string inputPath = $"{basePath}secret\\2small2.in";
            const string answerPath = $"{basePath}secret\\2small2.ans";

            TestSolverWithPaths(inputPath, answerPath);
        }

        [TestMethod]
        public void SolveSecret3Medium1()
        {
            const string inputPath = $"{basePath}secret\\3medium1.in";
            const string answerPath = $"{basePath}secret\\3medium1.ans";

            TestSolverWithPaths(inputPath, answerPath);
        }

        [TestMethod]
        public void SolveSecret4Medium2()
        {
            const string inputPath = $"{basePath}secret\\4medium2.in";
            const string answerPath = $"{basePath}secret\\4medium2.ans";

            TestSolverWithPaths(inputPath, answerPath);
        }

        [TestMethod]
        public void SolveSecret5Large1()
        {
            const string inputPath = $"{basePath}secret\\5large1.in";
            const string answerPath = $"{basePath}secret\\5large1.ans";

            TestSolverWithPaths(inputPath, answerPath);
        }

        [TestMethod]
        public void SolveSecret6Large2()
        {
            const string inputPath = $"{basePath}secret\\6large2.in";
            const string answerPath = $"{basePath}secret\\6large2.ans";

            TestSolverWithPaths(inputPath, answerPath);
        }

        private void TestSolverWithPaths(string inputPath, string answerPath)
        {
            using (FileInputDataSource dataSource = new FileInputDataSource(inputPath))
            {
                InputDataReader reader = new InputDataReader(dataSource);
                InputData input = reader.ReadInput();

                Graph graph = new Graph(input.Words);

                using (StreamReader expectedOutputReader = new StreamReader(answerPath))
                {
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
    }
}
