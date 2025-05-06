using Common;
using Common.DataSources;

namespace Lab3.Models
{
    public class InputData : IParsableInputData<InputData>
    {
        public int NumberOfPeople { get; }
        public int NumberOfConnections { get; }
        public List<Connection> Connections { get; }

        private InputData(int numberOfPeople, int numberOfConnections, List<Connection> connections)
        {
            NumberOfPeople = numberOfPeople;
            NumberOfConnections = numberOfConnections;
            Connections = connections;
        }

        public static InputData Parse(IInputDataSource source)
        {
            string? headerLine = source.ReadLine();

            if (headerLine == null)
                throw new InvalidOperationException("No input data.");

            string[] headerParts = headerLine.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            int n = int.Parse(headerParts[0]);
            int m = int.Parse(headerParts[1]);

            List<Connection> connections = new List<Connection>(m);

            for (int i = 0; i < m; i++)
            {
                string? line = source.ReadLine();

                if (line == null)
                    throw new InvalidOperationException($"Expected {m} connections but got fewer.");

                string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                int personA = int.Parse(parts[0]);
                int personB = int.Parse(parts[1]);
                int weight = int.Parse(parts[2]);

                connections.Add(new Connection(personA, personB, weight));
            }

            return new InputData(n, m, connections);
        }
    }
}
