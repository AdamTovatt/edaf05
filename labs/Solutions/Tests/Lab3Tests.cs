using Common;
using Common.DataSources;
using Lab3;
using Tests.Helpers;

namespace Tests
{
    [TestClass]
    public class Lab3Tests
    {
        private const string basePath = "..\\..\\..\\TestData\\Lab3";

        [DataTestMethod]
        [DataRow("sample", "1")]
        [DataRow("sample", "2")]
        [DataRow("secret", "1small")]
        [DataRow("secret", "2med")]
        [DataRow("secret", "3large")]
        [DataRow("secret", "4huge")]
        public void Solve_ReturnsCorrectAnswer(string subdir, string fileName)
        {
            TestCasePath path = TestCasePath.From(basePath, subdir, fileName);
            TestSolverWithPaths(path);
        }

        private void TestSolverWithPaths(TestCasePath path)
        {
            using FileInputDataSource input = new FileInputDataSource(path.InputPath);
            using StreamReader expected = new StreamReader(path.AnswerPath);

            SectionTimer sectionTimer = new SectionTimer();
            sectionTimer.StartSection("fullSolve", excludeFromTotalSum: true);

            int result = Program.Solve(input, sectionTimer);

            sectionTimer.StopSection("fullSolve");
            string? expectedOutput = expected.ReadLine();

            Assert.IsNotNull(expectedOutput, $"Missing expected output in: {Path.GetFileName(path.AnswerPath)}");

            string actualOutput = result.ToString();

            Assert.AreEqual(expectedOutput, actualOutput, $"Mismatch for test case: {Path.GetFileName(path.InputPath)}");

            Console.WriteLine(sectionTimer.ToString());
        }
    }
}
