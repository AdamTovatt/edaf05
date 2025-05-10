using Common;
using Common.DataSources;
using Lab5.Models;
using Lab5.Algorithms;

namespace Lab5
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IInputDataSource inputSource = new ConsoleInputDataSource();

            foreach (StringPair result in Solve(inputSource))
                Console.WriteLine($"{result.First} {result.Second}");
        }

        public static IEnumerable<StringPair> Solve(IInputDataSource inputSource, SectionTimer? sectionTimer = null)
        {
            sectionTimer?.StartSection("readInput");

            InputDataReader<InputData> reader = new InputDataReader<InputData>(inputSource);
            InputData input = reader.Read();

            sectionTimer?.StopSection("readInput");
            sectionTimer?.StartSection("solve");

            IEnumerable<StringPair> results = input.Queries
                .AsParallel()
                .AsOrdered() // Preserves the order of the input queries
                .Select(query => StringAligner.Align(query, input.Context))
                .ToList();

            sectionTimer?.StopSection("solve");

            return results;
        }
    }
}
