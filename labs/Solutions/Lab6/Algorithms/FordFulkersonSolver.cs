namespace Lab6.Algorithms
{
    public class FordFulkersonSolver : MaxFlowSolverBase
    {
        private readonly List<Edge>[] _adjacencyList;

        public FordFulkersonSolver(int nodeCount) : base(nodeCount)
        {
            _adjacencyList = new List<Edge>[nodeCount];

            for (int i = 0; i < nodeCount; i++)
                _adjacencyList[i] = new List<Edge>();
        }

        public override void AddUndirectedEdge(int from, int to, int capacity)
        {
            // For undirected graphs, add two directed edges with the same forward capacity
            // and zero residual capacity (residuals are managed automatically)
            AddResidualEdge(from, to, capacity);
            AddResidualEdge(to, from, capacity);
        }

        private void AddResidualEdge(int from, int to, int capacity)
        {
            Edge forward = new Edge(to, capacity);
            Edge backward = new Edge(from, 0);
            forward.Reverse = backward;
            backward.Reverse = forward;

            _adjacencyList[from].Add(forward);
            _adjacencyList[to].Add(backward);
        }

        public override int ComputeMaxFlow(int source, int sink)
        {
            int totalFlow = 0;

            while (true)
            {
                Edge[] path = new Edge[NodeCount];
                Queue<int> queue = new Queue<int>();
                queue.Enqueue(source);

                bool[] visited = new bool[NodeCount];
                visited[source] = true;

                while (queue.Count > 0 && !visited[sink])
                {
                    int current = queue.Dequeue();

                    foreach (Edge edge in _adjacencyList[current])
                    {
                        if (!visited[edge.To] && edge.Capacity > 0)
                        {
                            visited[edge.To] = true;
                            path[edge.To] = edge;
                            queue.Enqueue(edge.To);
                        }
                    }
                }

                if (!visited[sink])
                    break; // no augmenting path

                // Determine bottleneck (min capacity in path)
                int bottleneck = int.MaxValue;
                for (int node = sink; node != source;)
                {
                    Edge edge = path[node];
                    bottleneck = Math.Min(bottleneck, edge.Capacity);
                    node = edge.Reverse.To;
                }

                // Apply flow along the path
                for (int node = sink; node != source;)
                {
                    Edge edge = path[node];
                    edge.Capacity -= bottleneck;
                    edge.Reverse.Capacity += bottleneck;
                    node = edge.Reverse.To;
                }

                totalFlow += bottleneck;
            }

            return totalFlow;
        }

        private class Edge
        {
            public int To { get; }
            public int Capacity { get; set; }
            public Edge Reverse { get; set; } = null!;

            public Edge(int to, int capacity)
            {
                To = to;
                Capacity = capacity;
            }
        }
    }
}
