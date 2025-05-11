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

            Graph graph = Graph.CreateFromInputData(input);
            MaxFlowSolver flowSolver = new MaxFlowSolver(graph);

            int removedCount = 0;
            int allTimeMaxFlow = flowSolver.ComputeMaxFlow(graph.Nodes.First(), graph.Nodes.Last());

            foreach (int index in input.RemovalPlan)
            {
                graph.RemoveEdgeByIndex(index);

                int edgeCountInNodes = graph.Nodes.SelectMany(n => n.Edges).Distinct().Count();

                graph.ResetFlows();
                Console.WriteLine($"[DEBUG] Unique edges in Node.Edges after removal: {edgeCountInNodes}");

                flowSolver = new MaxFlowSolver(graph);
                int newFlow = flowSolver.ComputeMaxFlow(graph.Nodes.First(), graph.Nodes.Last());
                if (newFlow >= input.RequiredFlow)
                {
                    allTimeMaxFlow = newFlow;
                    removedCount++;
                }
                else
                {
                    break;
                }
            }

            sectionTimer?.StopSection("solve");
            return new SolveResult(removedCount, allTimeMaxFlow);
        }
    }
}
