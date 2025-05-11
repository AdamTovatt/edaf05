using Lab6.Models;

namespace Lab6.Algorithms
{
    public class MaxFlowSolver
    {
        public Graph Graph { get; set; }

        public MaxFlowSolver(Graph graph)
        {
            Graph = graph;
        }

        public int ComputeMaxFlow(Node source, Node sink)
        {
            int totalFlow = 0;

            Console.WriteLine($"Computing max flow from {source} to {sink}.");
            Console.WriteLine(Graph);

            while (true)
            {
                Console.WriteLine("Finding path...");
                FlowPath path = Graph.FindAugmentingPath(source, sink);

                Console.WriteLine("Path found:");
                Console.WriteLine(path);

                if (path.IsEmpty)
                    break;

                int flowToAdd = path.CalculateBottleneck();
                path.ApplyFlow(flowToAdd);
                totalFlow += flowToAdd;

                Console.WriteLine("Did apply the flow: " + flowToAdd);
                Console.WriteLine("Path after flow applied: ");
                Console.WriteLine(path);
            }

            return totalFlow;
        }
    }
}
