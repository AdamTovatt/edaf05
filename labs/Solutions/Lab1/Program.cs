using Lab1.Helpers;
using Lab1.Helpers.DataSources;
using Lab1.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        IInputDataSource dataSource = new ConsoleInputDataSource(); // the data source to read the input from
        InputDataReader reader = new InputDataReader(dataSource); // the reader that we want to read the input with
        InputData input = reader.ReadInput(); // the input that was read

        Graph graph = Graph.Create(input.Words); // create a graph, this is an expensive operation

        foreach (Query query in input.Queries) // run through all the queries
        {
            Node start = graph.GetNode(query.From);
            Node end = graph.GetNode(query.To);

            int? result = PathFinder.FindShortestPath(start, end); // will contain the distance for the shortest path or null if no path is found
            Console.WriteLine(result.HasValue ? result.Value.ToString() : "Impossible"); // write the result
        }
    }
}
