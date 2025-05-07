using Common;
using Common.DataSources;
using Lab4;
using Tests.Helpers;

namespace Tests
{
    [TestClass]
    public class Lab4Tests
    {
        private const string basePath = "..\\..\\..\\TestData\\Lab4";

        [DataTestMethod]
        [DataRow("sample", "1")]
        [DataRow("sample", "2")]
        [DataRow("more", "1")]
        [DataRow("more", "2")]
        [DataRow("more", "3")]
        [DataRow("more", "4")]
        [DataRow("more", "5")]
        [DataRow("secret", "0mini")]
        [DataRow("secret", "1small")]
        [DataRow("secret", "2med")]
        [DataRow("secret", "3large1")]
        [DataRow("secret", "4large2")]
        [DataRow("secret", "5larger")]
        [DataRow("secret", "6huge")]
        [DataRow("secret", "7huger")]
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

            double result = Program.Solve(input, sectionTimer);

            sectionTimer.StopSection("fullSolve");

            string? expectedOutput = expected.ReadLine();
            Assert.IsNotNull(expectedOutput, $"Missing expected output in: {Path.GetFileName(path.AnswerPath)}");

            string actualOutput = result.ToString("F6", System.Globalization.CultureInfo.InvariantCulture);

            Assert.AreEqual(expectedOutput, actualOutput, $"Mismatch for test case: {Path.GetFileName(path.InputPath)}");

            Console.WriteLine(sectionTimer.ToString());
        }
    }
}
