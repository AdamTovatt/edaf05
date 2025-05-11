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
            SolveResult result = Solve<FordFulkersonSolver>(inputSource);
            Console.WriteLine(result);
        }

        public static SolveResult Solve<TSolver>(IInputDataSource inputSource, SectionTimer? sectionTimer = null)
            where TSolver : MaxFlowSolverBase
        {
            sectionTimer?.StartSection("readInput");

            InputDataReader<InputData> reader = new InputDataReader<InputData>(inputSource);
            InputData input = reader.Read();

            sectionTimer?.StopSection("readInput");
            sectionTimer?.StartSection("solve");

            int currentFlow;
            int removedCount = 0;
            List<Edge> remainingEdges = new List<Edge>(input.Edges);

            // Initial max flow
            IMaxFlowSolver solver = SolverFactory.CreateSolver<TSolver>(input.NodeCount);

            foreach (Edge edge in remainingEdges)
                solver.AddUndirectedEdge(edge.From, edge.To, edge.Capacity);

            currentFlow = solver.ComputeMaxFlow(0, input.NodeCount - 1);

            // Try removing edges
            foreach (int index in input.RemovalPlan)
            {
                Edge toRemove = input.Edges[index];
                remainingEdges.Remove(toRemove);

                IMaxFlowSolver modifiedSolver = SolverFactory.CreateSolver<TSolver>(input.NodeCount);
                foreach (Edge edge in remainingEdges)
                    modifiedSolver.AddUndirectedEdge(edge.From, edge.To, edge.Capacity);

                int newFlow = modifiedSolver.ComputeMaxFlow(0, input.NodeCount - 1);
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
