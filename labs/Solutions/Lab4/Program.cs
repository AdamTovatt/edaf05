using Common;
using Common.DataSources;
using Lab4.Models;
using Lab4.Algorithms;

namespace Lab4
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IInputDataSource inputSource = new ConsoleInputDataSource();
            double result = Solve(inputSource);
            Console.WriteLine(result.ToString("F6", System.Globalization.CultureInfo.InvariantCulture));
        }

        public static double Solve(IInputDataSource inputSource, SectionTimer? sectionTimer = null)
        {
            sectionTimer?.StartSection("readInput");

            InputDataReader<InputData> reader = new InputDataReader<InputData>(inputSource);
            InputData input = reader.Read();

            sectionTimer?.StopSection("readInput");

            return ClosestPairSolver.FindClosestDistance(input.Points, sectionTimer);
        }
    }
}
