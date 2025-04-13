using Lab1.Helpers;
using Lab1.Helpers.DataSources;
using Lab1.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        IInputDataSource dataSource = new ConsoleInputDataSource();
        InputDataReader reader = new InputDataReader(dataSource);
        InputData input = reader.ReadInput();

        Graph graph = new Graph(input.Words);

        foreach (Query query in input.Queries)
        {
            Node start = graph.GetNode(query.From);
            Node end = graph.GetNode(query.To);

            int? result = PathFinder.FindShortestPath(start, end);
            Console.WriteLine(result.HasValue ? result.Value.ToString() : "Impossible");
        }
    }
}
