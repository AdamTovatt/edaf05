using Common;
using Common.DataSources;
using System.Globalization;

namespace Lab6.Models
{
    public class InputData : IParsableInputData<InputData>
    {
        public int NodeCount { get; }
        public int EdgeCount { get; }
        public int RequiredFlow { get; }
        public int PlanLength { get; }
        public List<Edge> Edges { get; }
        public List<int> RemovalPlan { get; }

        private InputData(int nodeCount, int edgeCount, int requiredFlow, int planLength, List<Edge> edges, List<int> removalPlan)
        {
            NodeCount = nodeCount;
            EdgeCount = edgeCount;
            RequiredFlow = requiredFlow;
            PlanLength = planLength;
            Edges = edges;
            RemovalPlan = removalPlan;
        }

        public static InputData Parse(IInputDataSource source)
        {
            string? firstLine = source.ReadLine();
            if (firstLine == null)
                throw new InvalidOperationException("Missing first line of input.");

            string[] firstParts = firstLine.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            int nodeCount = int.Parse(firstParts[0], CultureInfo.InvariantCulture);
            int edgeCount = int.Parse(firstParts[1], CultureInfo.InvariantCulture);
            int requiredFlow = int.Parse(firstParts[2], CultureInfo.InvariantCulture);
            int planLength = int.Parse(firstParts[3], CultureInfo.InvariantCulture);

            List<Edge> edges = new List<Edge>(edgeCount);

            for (int i = 0; i < edgeCount; i++)
            {
                string? line = source.ReadLine();
                if (line == null)
                    throw new InvalidOperationException($"Missing edge {i + 1}.");

                string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                int from = int.Parse(parts[0], CultureInfo.InvariantCulture);
                int to = int.Parse(parts[1], CultureInfo.InvariantCulture);
                int capacity = int.Parse(parts[2], CultureInfo.InvariantCulture);

                edges.Add(new Edge(from, to, capacity));
            }

            List<int> removalPlan = new List<int>(planLength);
            for (int i = 0; i < planLength; i++)
            {
                string? line = source.ReadLine();
                if (line == null)
                    throw new InvalidOperationException($"Missing removal index {i + 1}.");

                int index = int.Parse(line, CultureInfo.InvariantCulture);
                removalPlan.Add(index);
            }

            return new InputData(nodeCount, edgeCount, requiredFlow, planLength, edges, removalPlan);
        }
    }
}
