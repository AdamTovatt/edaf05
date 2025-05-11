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
            int allTimeMaxFlow = 0;

            while (true)
            {
                int currentMaxFlow = flowSolver.ComputeMaxFlow(graph.Nodes.First(), graph.Nodes.Last());

                if (currentMaxFlow >= input.RequiredFlow)
                {
                    allTimeMaxFlow = currentMaxFlow;

                    graph.RemoveEdgeByIndex(input.RemovalPlan[removedCount]);
                    removedCount++;

                    graph.ResetFlows();
                }
                else
                    break;
            }

            sectionTimer?.StopSection("solve");
            return new SolveResult(removedCount, allTimeMaxFlow);
        }
    }
}
