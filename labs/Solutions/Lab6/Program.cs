using Common;
using Common.DataSources;
using Lab6.Algorithms;
using Lab6.Models;

namespace Lab6
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            IInputDataSource inputSource = new ConsoleInputDataSource();
            SolveResult result = Solve(inputSource);
            Console.WriteLine(result);
        }

        public static SolveResult Solve(IInputDataSource inputSource, SectionTimer? sectionTimer = null)
        {
            sectionTimer?.StartSection("readInput");

            InputDataReader<InputData> reader = new InputDataReader<InputData>(inputSource);
            InputData input = reader.Read();

            sectionTimer?.StopSection("readInput");
            sectionTimer?.StartSection("solve");

            SolveResult result = FastMaxFlowSolver.Solve(input);

            sectionTimer?.StopSection("solve");
            return result;
        }
    }
}
