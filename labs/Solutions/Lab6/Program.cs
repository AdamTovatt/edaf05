using Common;
using Common.DataSources;
using Lab6.Models;
using Lab6.Algorithms;

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

            MaxFlowNetwork network = new MaxFlowNetwork(input.NodeCount);

            foreach (Edge edge in input.Edges)
                network.AddUndirectedEdge(edge.From, edge.To, edge.Capacity);

            int currentFlow = network.ComputeMaxFlow(0, input.NodeCount - 1);
            int removedCount = 0;

            foreach (int index in input.RemovalPlan)
            {
                Edge edgeToRemove = input.Edges[index];
                network.RemoveEdge(edgeToRemove.From, edgeToRemove.To);

                int newFlow = network.ComputeMaxFlow(0, input.NodeCount - 1);
                if (newFlow < input.RequiredFlow)
                    break;

                removedCount++;
                currentFlow = newFlow;
            }

            sectionTimer?.StopSection("solve");
            return new SolveResult(removedCount, currentFlow);
        }
    }
}
