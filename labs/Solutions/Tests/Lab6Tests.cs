using Common;
using Common.DataSources;
using Lab6;
using Lab6.Algorithms;
using Lab6.Models;
using Tests.Helpers;

namespace Tests
{
    [TestClass]
    public class Lab6Tests
    {
        private const string basePath = "..\\..\\..\\TestData\\Lab6";

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
            sectionTimer.StartSection("fullSolve", excludeFromTotalSum: true);

            SolveResult result = Program.Solve<FordFulkersonSolver>(inputSource, sectionTimer);

            sectionTimer.StopSection("fullSolve");

            string? expected = expectedOutputReader.ReadLine();

            Assert.IsNotNull(expected);

            expected = expected.Trim();
            string actual = result.ToString().Trim();

            Assert.AreEqual(expected, actual, $"Mismatch in test case: {Path.GetFileName(path.InputPath)}");

            Console.WriteLine(sectionTimer.ToString());
        }
    }
}
