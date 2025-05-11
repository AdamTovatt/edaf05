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

            while (true)
            {
                FlowPath path = Graph.FindAugmentingPath(source, sink);
                if (path.IsEmpty)
                    break;

                int flowToAdd = path.CalculateBottleneck();
                path.ApplyFlow(flowToAdd);
                totalFlow += flowToAdd;
            }

            return totalFlow;
        }
    }
}
