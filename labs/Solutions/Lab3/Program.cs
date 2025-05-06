using Common;
using Common.DataSources;
using Lab3.Models;
using Lab3.Algorithms;

namespace Lab3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IInputDataSource inputSource = new ConsoleInputDataSource();
            int result = Solve(inputSource);
            Console.WriteLine(result);
        }

        public static int Solve(IInputDataSource inputSource)
        {
            InputDataReader<InputData> reader = new InputDataReader<InputData>(inputSource);
            InputData input = reader.Read();

            return KruskalSolver.ComputeMinimumTotalWeight(input);
        }
    }
}
