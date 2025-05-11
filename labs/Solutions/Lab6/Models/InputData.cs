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
        public List<InputEdge> Edges { get; }
        public List<int> RemovalPlan { get; }

        private InputData(int nodeCount, int edgeCount, int requiredFlow, int planLength, List<InputEdge> edges, List<int> removalPlan)
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
            string sourceLine = GetLine(source, "Missing first line of input.");

            string[] firstParts = sourceLine.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            int nodeCount = GetPartAsInt(firstParts, 0);
            int edgeCount = GetPartAsInt(firstParts, 1);
            int requiredFlow = GetPartAsInt(firstParts, 2);
            int planLength = GetPartAsInt(firstParts, 3);

            List<InputEdge> edges = new List<InputEdge>(edgeCount);

            for (int i = 0; i < edgeCount; i++)
            {
                sourceLine = GetLine(source, $"Missing edge {i + 1}.");

                edges.Add(InputEdge.FromString(sourceLine));
            }

            List<int> removalPlan = new List<int>(planLength);
            for (int i = 0; i < planLength; i++)
                removalPlan.Add(GetInt(GetLine(source, $"Missing removal index {i + 1}.")));

            return new InputData(nodeCount, edgeCount, requiredFlow, planLength, edges, removalPlan);
        }

        private static string GetLine(IInputDataSource source, string errorMessage)
        {
            string? line = source.ReadLine();

            if (line == null)
                throw new InvalidOperationException(errorMessage);

            return line;
        }

        private static int GetPartAsInt(string[] parts, int index)
        {
            return int.Parse(parts[index], CultureInfo.InvariantCulture);
        }

        private static int GetInt(string originalString)
        {
            return int.Parse(originalString, CultureInfo.InvariantCulture);
        }
    }
}
